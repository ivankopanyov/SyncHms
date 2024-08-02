namespace SyncHms.Services.Services.Implement;

internal class SanatoriumService : ISanatoriumService
{
    private readonly SemaphoreSlim _semaphore = new(1);

    private readonly IControl<SanatoriumOptions, ApplicationEnvironment> _control;

    private readonly IMemoryCache _memoryCache;

    private IEndpointInstance? _endpointInstance;

    private CancellationTokenSource _cancellationTokenSource = new();

    private CancellationToken _cancellationToken;

    public ApplicationEnvironment Environment => _control.Environment;
    
    public event PostingRequestHandle? PostingRequestEvent;

    public SanatoriumService(IControl<SanatoriumOptions, ApplicationEnvironment> control, IMemoryCache memoryCache)
    {
        _control = control;
        _memoryCache = memoryCache;
        _cancellationToken = _cancellationTokenSource.Token;
    }

    public Task ChangedOptionsHandleAsync(SanatoriumOptions options)
    {
        new Thread(Connect).Start(options);
        throw new Exception(options.Enabled
            ? "Restarting the service."
            : "Service is disabled");
    }

    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current,
        ApplicationEnvironment previous) => Task.CompletedTask;
    
    public async Task SendReservationUpdatedMessageAsync(ReservationUpdatedMessage message)
    {
        if (!_control.Options.Enabled)
            throw new InvalidOperationException("Service is disabled.");
        
        try
        {
            await _endpointInstance.Publish(message);
            _control.Active();
        }
        catch (Exception ex)
        {
            _control.Unactive(ex);
            throw;
        }
    }

    public async Task SendPostTransactionsRequestAsync(PostTransactionsRequest message,
        IMessageHandlerContext context, TimeSpan timeout, CancellationTokenSource cancellationTokenSource)
    {
        if (!_control.Options.Enabled)
            return;
        
        await _memoryCache.PushAsync(message.CorrelationId, new MessageContextSource
        {
            Context = context,
            CancellationTokenSource = cancellationTokenSource
        }, timeout);
        
        PostingRequestEvent?.Invoke(message);
    }

    public async Task SendPostTransactionsResponseAsync(PostTransactionsResponse message)
    {
        if (!_control.Options.Enabled)
            throw new InvalidOperationException("Service is disabled.");
        
        try
        {
            if (await _memoryCache.PopAsync<MessageContextSource>(message.CorrelationId) is not { } source)
                throw new TimeoutException("Response timed out.");
            
            await source.Context.Reply(message);
            _control.Active();
            await source.CancellationTokenSource.CancelAsync();
        }
        catch (Exception ex)
        {
            _control.Unactive(ex);
            throw;
        }
    }

    private async void Connect(object? newOptions)
    {
        var options = (SanatoriumOptions)newOptions!;
        await _cancellationTokenSource.CancelAsync();

        await _semaphore.WaitAsync();

        try
        {
            if (_endpointInstance != null)
            {
                try
                {
                    await _endpointInstance.Stop();
                }
                catch
                {
                    // ignored
                }
            }

            if (!options.Enabled) return;

            if (_cancellationToken.IsCancellationRequested)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _cancellationToken = _cancellationTokenSource.Token;
            }

            await Task.Delay(TimeSpan.FromSeconds(Math.Max(_control.Options.ConnectionDelay, 0)), _cancellationToken);

            var endpointConfiguration = new EndpointConfiguration(options.Endpoint);
            endpointConfiguration.AssemblyScanner().ExcludeAssemblies("Logus.HMS.Messages");
            if (!string.IsNullOrEmpty(options.License)) endpointConfiguration.License(options.License);
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.UseTransport<SqlServerTransport>().ConnectionString(options.ConnectionString);
            endpointConfiguration.PurgeOnStartup(false);
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.RegisterComponents(services => services.RegisterSingleton<ISanatoriumService>(this));

            _endpointInstance = await Task.Run(async () => await Endpoint.Start(endpointConfiguration))
                .WaitAsync(_cancellationToken);

            _control.Active();
        }
        catch (OperationCanceledException ex)
        {
            _control.Unactive(_control.Options.Enabled
                ? ex
                : new Exception("Service is disabled"));
        }
        catch (Exception ex)
        {
            _control.Unactive(ex.Message.Split('\n')[0]);
            new Thread(Connect).Start(options);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
