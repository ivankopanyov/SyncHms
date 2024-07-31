namespace SyncHms.Bus.Services.Services.Implement;

internal class ServiceControllerStarter : BackgroundService
{
    private readonly IServiceController _serviceController;

    public ServiceControllerStarter(IServiceController serviceController, IBusProvider provider)
    {
        _serviceController = serviceController;

        serviceController.SetOptionsEvent += async options => await provider.PublishAsync(options);
        serviceController.ReloadEvent += async serviceName => await provider.PublishAsync(new Reload
        {
            ServiceName = serviceName
        });
        
        provider
            .Subscribe<UpdatedServiceInfo, ServiceControllerStarter>(UpdatedServiceInfoHandle);
    }
    
    private Task UpdatedServiceInfoHandle(UpdatedServiceInfo serviceInfo, IMessageContext context)
    {
        _serviceController.ChangedOptions(serviceInfo);
        return Task.CompletedTask;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}

internal class ServiceControllerStarter<TEnvironment> : ServiceControllerStarter where TEnvironment : class, new()
{
    public ServiceControllerStarter(IServiceController<TEnvironment> serviceController, IBusProvider provider)
        : base(serviceController, provider)
    {
        serviceController.SetEnvironmentEvent += async environment =>
        {
            await provider.PublishAsync(environment);
        };
    }
}
