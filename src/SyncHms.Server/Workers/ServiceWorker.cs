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

    /// <summary>Экземпляр планировщика событий.</summary>
    private readonly IEventScheduler _eventScheduler;

    /// <summary>Инициализация сервиса.</summary>
    /// <param name="serviceScopeFactory">
    /// Экземпляр фабрики, создающей объекты, реализующие интерфейс <see cref="IServiceScopeFactory"/>
    /// </param>
    /// <param name="environment">Экземпляр окружения.</param>
    /// <param name="serviceController">Экземпляр контроллера сервисов.</param>
    /// <param name="serviceStatePublisher">Сервис публикации событий <see cref="ChangedServiceState"/></param>
    /// <param name="eventScheduler">Экземпляр планировщика событий.</param>
    public ServiceWorker(IServiceScopeFactory serviceScopeFactory,
        IOptions<ApplicationEnvironment>? environment,
        IServiceController<ApplicationEnvironment> serviceController,
        IEventPublisher<ChangedServiceState> serviceStatePublisher,
        IEventScheduler eventScheduler)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _environment = environment;
        _serviceController = serviceController;
        _serviceStatePublisher = serviceStatePublisher;
        _eventScheduler = eventScheduler;

        _serviceController.ChangedOptionsEvent += async serviceInfo => await HandleAsync(serviceInfo);
        _eventScheduler.UpdateScheduleEvent += UpdateScheduleHandleAsync;
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
    /// Отправляет сервисам приложения экземпляр окружения и обновляет опций планируемых событий в базе данных.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        await UpdateEnvironmentAsync(scope);
        await UpdateSchedulesAsync(scope);
    }

    /// <summary>Метод отправляет сервисам приложения экземпляр окружения.</summary>
    private async Task UpdateEnvironmentAsync(IServiceScope scope)
    {
        var environmentRepository = scope.ServiceProvider
            .GetRequiredService<IEnvironmentRepository<ApplicationEnvironment>>();

        if (await environmentRepository.GetAsync() is not { } environment)
        {
            environment = _environment?.Value ?? new ApplicationEnvironment();
            await environmentRepository.UpdateAsync(environment);
        }
        
        _serviceController.SetEnvironment(environment);
    }

    /// <summary>Метод обновляет опций планируемых событий в базе данных.</summary>
    private async Task UpdateSchedulesAsync(IServiceScope scope)
    {
        var scheduleRepository = scope.ServiceProvider.GetRequiredService<IScheduleRepository>();
        var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<ScheduleHub>>();
        HashSet<string> exists = [];

        foreach (var s in _eventScheduler.Schedules)
        {
            exists.Add(s.Key);

            if (await scheduleRepository.GetAsync(s.Key) is { } schedule)
            {
                schedule.Description = s.Value.Description;
                await scheduleRepository.UpdateAsync(schedule);
                await _eventScheduler.UpdateScheduleAsync(schedule.Name,
                    TimeSpan.FromSeconds(schedule.IntervalSeconds), schedule.Last);
            }
            else
            {
                schedule = new Schedule
                {
                    Name = s.Key,
                    Description = s.Value.Description,
                    IntervalSeconds = (int)s.Value.Interval.TotalSeconds,
                    Last = s.Value.Last
                };

                await scheduleRepository.UpdateAsync(schedule);
            }

            await hubContext.Clients.All.SendAsync("Schedule", schedule);
        }

        foreach (var s in await scheduleRepository.GetAllAsync())
        {
            if (!exists.Remove(s.Name))
            {
                await scheduleRepository.RemoveAsync(s.Name);
                await hubContext.Clients.All.SendAsync("RemoveSchedule", new RemoveSchedule
                {
                    ScheduleName = s.Name
                });
            }
        }
    }

    /// <summary>
    /// Метод, обрабатывающий сообщения типа <see cref="ScheduleOptions"/>,
    /// отправленные планировщиком событий, хранящие текущее опции планируемого события.
    /// </summary>
    /// <param name="scheduleName">Имя планируемого события.</param>
    /// <param name="options">Опции планируемого события.</param>
    private async Task UpdateScheduleHandleAsync(string scheduleName, ScheduleOptions options)
    {
        var schedule = new Schedule
        {
            Name = scheduleName,
            Description = options.Description,
            IntervalSeconds = (int)options.Interval.TotalSeconds,
            Last = options.Last
        };

        using var scope = _serviceScopeFactory.CreateScope();
        var scheduleRepository = scope.ServiceProvider.GetRequiredService<IScheduleRepository>();
        var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<ScheduleHub>>();

        await scheduleRepository.UpdateAsync(schedule);
        await hubContext.Clients.All.SendAsync("Schedule", schedule);
    }
}
