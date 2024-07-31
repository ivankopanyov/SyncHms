namespace SyncHms.Server.Handlers;

public class ServiceHandler : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly IServiceController<ApplicationEnvironment> _serviceController;

    public ServiceHandler(IServiceScopeFactory serviceScopeFactory,
        IServiceController<ApplicationEnvironment> serviceController)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _serviceController = serviceController;

        _serviceController.ChangedOptionsEvent += async serviceInfo => await HandleAsync(serviceInfo);
    }

    private async Task HandleAsync(UpdatedServiceInfo serviceInfo)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var serviceRepository = scope.ServiceProvider.GetRequiredService<IServiceRepository>();
        var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<ServiceHub>>();

        var service = new Service
        {
            Name = serviceInfo.Name,
            JsonOptions = serviceInfo.JsonOptions,
            State = serviceInfo.State == null ? null : new State
            {
                IsActive = serviceInfo.State.IsActive,
                Error = serviceInfo.State.Error,
                StackTrace = serviceInfo.State.StackTrace,
                Info = serviceInfo.State.Info,
            }
        };
            
        var result = await serviceRepository.UpdateAsync(service, serviceInfo.UpdateOptions);
        await hubContext.Clients.All.SendAsync("Service", result);

        if (serviceInfo.ResponseRequired)
        {
            if (serviceInfo.JsonOptions == result.JsonOptions)
            {
                _serviceController.Reload(result.Name);
            }
            else
            {
                _serviceController.SetOptions(new Bus.Services.Options
                {
                    ServiceName = serviceInfo.Name,
                    JsonOptions = result.JsonOptions
                });
            }
        }
        else if (!serviceInfo.UpdateOptions && serviceInfo.JsonOptions != result.JsonOptions)
        {
            _serviceController.SetOptions(new Bus.Services.Options
            {
                ServiceName = serviceInfo.Name,
                JsonOptions = result.JsonOptions
            });
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var environmentRepository = scope.ServiceProvider
            .GetRequiredService<IEnvironmentRepository<ApplicationEnvironment>>();

        if (await environmentRepository.GetAsync() is not { } environment)
        {
            environment = new ApplicationEnvironment();
            await environmentRepository.UpdateAsync(environment);
        }
        
        _serviceController.SetEnvironment(environment);
    }
}
