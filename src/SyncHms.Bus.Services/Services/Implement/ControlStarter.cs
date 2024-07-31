namespace SyncHms.Bus.Services.Services.Implement;

internal class ControlStarter<TService, TOptions> : BackgroundService
    where TService : IService<TOptions> where TOptions : class, new()
{
    private readonly JsonSerializerSettings _settings = new()
    {
        ContractResolver = new DescriptionContractResolver()
    };

    private readonly ILogger _logger;

    private readonly IControl<TOptions> _control;

    private readonly string _serviceName;

    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly IBusProvider _provider;

    private readonly SemaphoreSlim _semaphore = new(1);

    private bool _isActive;

    private Exception? _currentException;

    public ControlStarter(IServiceScopeFactory serviceScopeFactory, IControl<TOptions> control,
        ServiceOptions<TOptions> options, IBusProvider provider, ILogger<TService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _control = control;
        _serviceName = options.ServiceName;
        _provider = provider;

        _control.ActiveEvent += ActiveAsync;
        _control.UnactiveEvent += async (error, ex) => await UnactiveAsync(ex ?? new Exception(error));

        provider
            .Subscribe<Options, TService>(SetOptionsAsync)
            .Subscribe<Reload, TService>(ChangedOptionsHandleAsync);
    }

    private async Task ActiveAsync()
    {
        await _semaphore.WaitAsync();
        
        try
        {
            if (_isActive && _currentException == null)
                return;

            _isActive = true;
            _currentException = null;
            _logger.LogActive(_serviceName);
            await SendOptionsAsync(updateOptions: false);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task UnactiveAsync(Exception ex)
    {
        await _semaphore.WaitAsync();

        try
        {
            if (!_isActive && _currentException != null && _currentException.Message == ex.Message)
                return;

            _isActive = false;
            _currentException = ex;
            _logger.LogUnactive(_serviceName, ex.Message, ex);
            await SendOptionsAsync(updateOptions: false);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task SetOptionsAsync(Options configuration, IMessageContext context)
    {
        if (configuration.ServiceName != _serviceName)
            return;
        
        if (configuration.JsonOptions == null)
        {
            await SendOptionsAsync("Options is null.", false);
            return;
        }

        try
        {
            if (JsonConvert.DeserializeObject<TOptions>(configuration.JsonOptions, _settings) is not { } options)
            {
                await SendOptionsAsync("Failed to deserialize options.", false);
                return;
            }

            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(options, new ValidationContext(options), results, true))
            {
                await SendOptionsAsync(string.Join(' ', results.Select(result => result.ErrorMessage)), false);
                return;
            }

            bool equals;

            await _semaphore.WaitAsync();
            
            try
            {
                equals = _control.Options.Equals(options);
                _control.Options = options;
                await SendOptionsAsync();
            }
            finally
            {
                _semaphore.Release();
            }

            if (!equals)
                await ChangedOptionsHandleAsync(options);
        }
        catch (Exception ex)
        {
            await _semaphore.WaitAsync();

            try
            {
                await SendOptionsAsync(ex.Message, false);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }

    private async Task SendOptionsAsync(string? info = null,
        bool updateOptions = true, bool responseRequired = false)
    {
        var serviceInfo = new UpdatedServiceInfo
        {
            ResponseRequired = responseRequired
        };
        WriteServiceInfo(serviceInfo, updateOptions, info);
        await _provider.PublishAsync(serviceInfo);
    }

    private void WriteServiceInfo(UpdatedServiceInfo serviceInfo, bool updateOptions, string? info = null)
    {
        try
        {
            serviceInfo.JsonOptions = JsonConvert.SerializeObject(_control.Options, _settings);
        }
        catch (Exception ex)
        {
            if (info == null)
                info = ex.Message;
            else
                info += $" {ex.Message}";
        }
        
        serviceInfo.Name = _serviceName;
        serviceInfo.UpdateOptions = updateOptions;
        serviceInfo.State = new ServiceState
        {
            IsActive = _isActive,
            Error = _currentException?.Message,
            StackTrace = _currentException?.StackTrace,
            Info = info
        };
    }

    private async Task ChangedOptionsHandleAsync(Reload reload, IMessageContext context)
    {
        if (reload.ServiceName != _serviceName)
            return;

        await ChangedOptionsHandleAsync(_control.Options);
    }

    private async Task ChangedOptionsHandleAsync(TOptions options)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<TService>();

        try
        {
            await service.ChangedOptionsHandleAsync(options);
            _control.Active();
        }
        catch (Exception ex)
        {
            _control.Unactive(ex);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await SendOptionsAsync(updateOptions: false, responseRequired: true);
    }
}

internal class ControlStarter<TService, TOptions, TEnvironment> : ControlStarter<TService, TOptions>
    where TService : IService<TOptions, TEnvironment> where TOptions : class, new() where TEnvironment : class, new()
{
    private readonly IControl<TOptions, TEnvironment> _control;

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ControlStarter(IServiceScopeFactory serviceScopeFactory, IControl<TOptions, TEnvironment> control,
        ServiceOptions<TOptions> options, IBusProvider provider, ILogger<TService> logger)
        : base(serviceScopeFactory, control, options, provider, logger)
    {
        _control = control;
        _serviceScopeFactory = serviceScopeFactory;
        
        provider
            .Subscribe<TEnvironment, TService>(ChangeEnvironmentHandleAsync);
    }

    private async Task ChangeEnvironmentHandleAsync(TEnvironment environment, IMessageContext context)
    {
        var previous = _control.Environment;
        _control.Environment = environment;

        using var scope = _serviceScopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<TService>();

        try
        {
            await service.ChangedEnvironmentHandleAsync(environment, previous);
        }
        catch (Exception ex)
        {
            _control.Unactive(ex);
        }
    }
}
