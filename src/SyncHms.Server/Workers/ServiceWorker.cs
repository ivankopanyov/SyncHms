namespace SyncHms.Server.Workers;

/// <summary>
/// Класс, описывающий фоновый сервис, отправляющий в шину данных
/// экземпляры опций сервисов и экземпляр окружения из базы данных.<br/>
/// Унаследован от класса <see cref="BackgroundService"/>
/// </summary>
public class ServiceWorker : BackgroundService
{
    /// <summary>Экземпляр фабрики, создающей объекты, реализующие интерфейс <see cref="IServiceScopeFactory"/></summary>
    private readonly IServiceScopeFactory _serviceScopeFactory;

    /// <summary>Экземпляр окружения.</summary>
    private readonly IOptions<ApplicationEnvironment>? _environment;

    /// <summary>Экземпляр контроллера сервисов.</summary>
    private readonly IServiceController<ApplicationEnvironment> _serviceController;

    /// <summary>Сервис публикации событий <see cref="ChangedServiceState"/></summary>
    private readonly IEventPublisher<ChangedServiceState> _serviceStatePublisher;

    /// <summary>Инициализация сервиса.</summary>
    /// <param name="serviceScopeFactory">
    /// Экземпляр фабрики, создающей объекты, реализующие интерфейс <see cref="IServiceScopeFactory"/>
    /// </param>
    /// <param name="environment">Экземпляр окружения.</param>
    /// <param name="serviceController">Экземпляр контроллера сервисов.</param>
    /// <param name="serviceStatePublisher">Сервис публикации событий <see cref="ChangedServiceState"/></param>
    public ServiceWorker(IServiceScopeFactory serviceScopeFactory,
        IOptions<ApplicationEnvironment>? environment,
        IServiceController<ApplicationEnvironment> serviceController,
        IEventPublisher<ChangedServiceState> serviceStatePublisher)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _environment = environment;
        _serviceController = serviceController;
        _serviceStatePublisher = serviceStatePublisher;

        _serviceController.ChangedOptionsEvent += async serviceInfo => await HandleAsync(serviceInfo);
    }

    /// <summary>
    /// Метод, обрабатывающий сообщения типа <see cref="UpdatedServiceInfo"/>,
    /// отправленные сервисами приложения, хранящие текущее состояние и опции сервиса.
    /// </summary>
    /// <param name="serviceInfo">Экземпляр сообщения.</param>
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

        if (result.State != null)
        {
            _serviceStatePublisher.Publish(new ChangedServiceState
            {
                ServiceName = result.Name,
                Connection = result.State.IsActive,
                Message = result.State.Error
            });
        }

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

    /// <summary>
    /// Метод, переопределяющий базовый метод <see cref="BackgroundService.ExecuteAsync"/><br/>
    /// Отправляет сервисам приложения экземпляр окружения.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var environmentRepository = scope.ServiceProvider
            .GetRequiredService<IEnvironmentRepository<ApplicationEnvironment>>();

        if (await environmentRepository.GetAsync() is not { } environment)
        {
            environment = _environment?.Value ?? new ApplicationEnvironment();
            await environmentRepository.UpdateAsync(environment);
        }
        
        _serviceController.SetEnvironment(environment);
    }
}
