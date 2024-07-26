namespace SyncHms.Bus.Services.Services.Implement;

internal class ServiceControllerStarter : BackgroundService
{
    private protected readonly IServiceController _serviceController;
    
    private protected readonly IBusProvider _provider;

    public ServiceControllerStarter(IServiceController serviceController, IBusProvider provider)
    {
        _serviceController = serviceController;
        _provider = provider;

        serviceController.SetOptionsEvent += async options => await _provider.PublishAsync(options);

        serviceController.ReloadEvent += async serviceName => await _provider.PublishAsync(new Reload
        {
            ServiceName = serviceName
        });
        
        _provider.Subscribe<UpdatedServiceInfo, ServiceControllerStarter>(UpdatedServiceInfoHandle);
    }
    
    private Task UpdatedServiceInfoHandle(UpdatedServiceInfo serviceInfo, IMessageContext context)
    {
        _serviceController.ChangedOptions(serviceInfo);
        return Task.CompletedTask;
    }

    protected sealed override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _provider.PublishAsync(new ServiceControllerStarted());
    }
}

internal class ServiceControllerStarter<TEnvironment> : ServiceControllerStarter where TEnvironment : class, new()
{
    private TEnvironment? _environment;

    public ServiceControllerStarter(IServiceController<TEnvironment> serviceController, IBusProvider provider)
        : base(serviceController, provider)
    {
        serviceController.SetEnvironmentEvent += async environment =>
        {
            _environment = environment;
            await _provider.PublishAsync(_environment);
        };
        
        _provider.Subscribe<UpdatedServiceInfoEnvironment, ServiceControllerStarter>(UpdatedServiceInfoHandleAsync);
    }

    private async Task UpdatedServiceInfoHandleAsync(UpdatedServiceInfoEnvironment serviceInfo, IMessageContext context)
    {
        _serviceController.ChangedOptions(serviceInfo);
        if (serviceInfo.RequestEnvironment && _environment != null)
        {
            await _provider.PublishAsync(new Environment<TEnvironment>
            {
                ServiceName = serviceInfo.Name,
                Value = _environment
            });
        }
    }
}
