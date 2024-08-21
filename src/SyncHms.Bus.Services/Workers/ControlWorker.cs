namespace SyncHms.Bus.Services.Workers;

/// <summary>
/// Класс, описывающий фоновый сервис, реализующий методы обработки событий сервиса.<br/>
/// Унаследован от класса <see cref="BackgroundService"/>
/// </summary>
/// <typeparam name="TService">
/// Тип сервиса. Должен реализовывать интерфейс <see cref="IService{TOptions}"/>
/// </typeparam>
/// <typeparam name="TOptions">
/// Тип опций сервиса. Должен иметь открытый конструктор без параметров.
/// </typeparam>
internal class ControlWorker<TService, TOptions> : BackgroundService
    where TService : IService<TOptions> where TOptions : class, new()
{
    /// <summary>Экземпляр настроек сериализации и десериализации опций сервиса.</summary>
    private readonly JsonSerializerSettings _settings = new()
    {
        ContractResolver = new DescriptionContractResolver()
    };

    /// <summary>Экземпляр логгера.</summary>
    private readonly ILogger _logger;
    
    /// <summary>Экземпляр контроллера сервиса.</summary>
    private readonly IControl<TOptions> _control;

    /// <summary>Имя сераиса.</summary>
    private readonly string _serviceName;

    /// <summary>Экземпляр фабрики, создающей объекты <see cref="IServiceScope"/></summary>
    private readonly IServiceScopeFactory _serviceScopeFactory;

    /// <summary>Экземпляр провайдера шины данных.</summary>
    private readonly IBusProvider _provider;

    /// <summary>Экземпляр семафора, блокирующий отправку некорректного состояния сервиса.</summary>
    private readonly SemaphoreSlim _semaphore = new(1);

    /// <summary>Флаг, хранящий состояние подключения сервиса к удаленному ресурсу.</summary>
    private bool _isActive;

    /// <summary>Исключение, возбужденное во время последней попытки подключения сервиса к удаленному ресурсу.</summary>
    private Exception? _currentException;

    /// <summary>Инициализация сервиса.</summary>
    /// <param name="serviceScopeFactory">Экземпляр фабрики, создающей объекты <see cref="IServiceScope"/></param>
    /// <param name="control">Экземпляр контроллера сервиса.</param>
    /// <param name="options">Экземпляр опций сервиса.</param>
    /// <param name="provider">Экземпляр провайдера шины данных.</param>
    /// <param name="logger">Экземпляр логгера.</param>
    public ControlWorker(IServiceScopeFactory serviceScopeFactory, IControl<TOptions> control,
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

    /// <summary>Метод, отправляющий событие о подключении севриса к удаленному ресурсу в шину данных.</summary>
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

    /// <summary>
    /// Метод, отправляющий событие о неудачной попытке подключения севриса к удаленному ресурсу в шину данных.
    /// </summary>
    /// <param name="ex">Исключение, возбужденное во время попытки подключения.</param>
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

    /// <summary>Метод, обрабатывающий обновленные опции сервиса.</summary>
    /// <param name="configuration">Обновленные опции сервиса.</param>
    /// <param name="context">Контекст обработки сообщения.</param>
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

    /// <summary>Метод, отправляющий состояние сервиса в шину данных.</summary>
    /// <param name="info">Сообщение об ошибке во время обработки обновленных опций сервиса.</param>
    /// <param name="updateOptions">
    /// Флаг, указывающий контроллеру сервисов перезаписать опции сервиса в базе данных опциями, в текущем сообщении.
    /// </param>
    /// <param name="responseRequired">
    /// Флаг, указывающий контролерру сервисов об обязательном ответе на сообщение.
    /// </param>
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

    /// <summary>Метод, формирующий сообщение о текущем состоянии сервиса для отправки в шину данных.</summary>
    /// <param name="serviceInfo">Экземпляр, содержащий текущее состояние сервиса.</param>
    /// <param name="updateOptions">
    /// Флаг, указывающий контроллеру сервисов перезаписать опции сервиса в базе данных опциями, в текущем сообщении.
    /// </param>
    /// <param name="info">Сообщение об ошибке во время обработки обновленных опций сервиса.</param>
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

    /// <summary>Метод, обрабатывающий событие переподключения сервиса к удаленному ресурсу.</summary>
    /// <param name="reload">Экземпляр сообщения о переподключении.</param>
    /// <param name="context">Контекст обработки сообщения.</param>
    private async Task ChangedOptionsHandleAsync(Reload reload, IMessageContext context)
    {
        if (reload.ServiceName != _serviceName)
            return;

        await ChangedOptionsHandleAsync(_control.Options);
    }

    /// <summary>Метод, обрабатывающий событие запроса на изменения опций сервиса.</summary>
    /// <param name="options">Обновленные опции сервиса.</param>
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

    /// <summary>
    /// Метод отправляет запрос контроллеру сервисов на возвращение ему опций из базы данных.<br/>
    /// Переопределяет метод <see cref="BackgroundService.ExecuteAsync"/>
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await SendOptionsAsync(updateOptions: false, responseRequired: true);
    }
}

/// <summary>
/// Класс, описывающий фоновый сервис, реализующий методы обработки событий сервиса.<br/>
/// Унаследован от класса <see cref="ControlWorker{TService, TOptions}"/>
/// </summary>
/// <typeparam name="TService">
/// Тип сервиса. Должен реализовывать интерфейс <see cref="IService{TOptions, TEnvironment}"/>
/// </typeparam>
/// <typeparam name="TOptions">
/// Тип опций сервиса. Должен иметь открытый конструктор без параметров.
/// </typeparam>
/// <typeparam name="TEnvironment">
/// Тип окружения. Должен иметь открытый конструктор без параметров.
/// </typeparam>
internal class ControlWorker<TService, TOptions, TEnvironment> : ControlWorker<TService, TOptions>
    where TService : IService<TOptions, TEnvironment> where TOptions : class, new() where TEnvironment : class, new()
{
    /// <summary>Экземпляр контроллера сервиса.</summary>
    private readonly IControl<TOptions, TEnvironment> _control;

    /// <summary>Экземпляр фабрики, создающей объекты <see cref="IServiceScope"/></summary>
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    /// <summary>Инициализация сервиса.</summary>
    /// <param name="serviceScopeFactory">Экземпляр фабрики, создающей объекты <see cref="IServiceScope"/></param>
    /// <param name="control">Экземпляр контроллера сервиса.</param>
    /// <param name="options">Экземпляр опций сервиса.</param>
    /// <param name="provider">Экземпляр провайдера шины данных.</param>
    /// <param name="logger">Экземпляр логгера.</param>
    public ControlWorker(IServiceScopeFactory serviceScopeFactory, IControl<TOptions, TEnvironment> control,
        ServiceOptions<TOptions> options, IBusProvider provider, ILogger<TService> logger)
        : base(serviceScopeFactory, control, options, provider, logger)
    {
        _control = control;
        _serviceScopeFactory = serviceScopeFactory;
        
        provider
            .Subscribe<TEnvironment, TService>(ChangeEnvironmentHandleAsync);
    }

    /// <summary>Метод, обрабатывающий сообщение об изменении значений переменных окружения.</summary>
    /// <param name="environment">Обновленное окружение.</param>
    /// <param name="context">Контекст обработки сообщения.</param>
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
