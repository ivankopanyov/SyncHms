namespace SyncHms.Bus.Services.Workers;

/// <summary>
/// Класс, описывающий фоновый сервис, подписывающий на события <see cref="IServiceController"/><br/>
/// Унаследован от класса <see cref="BackgroundService"/>
/// </summary>
internal class ServiceControllerWorker : BackgroundService
{
    /// <summary>Экземпляр контроллер сервисов.</summary>
    private readonly IServiceController _serviceController;

    /// <summary>Инициализация сервиса.</summary>
    /// <param name="serviceController">Экземпляр контроллер сервисов.</param>
    /// <param name="provider">Экземпляр провайдера шины данных.</param>
    public ServiceControllerWorker(IServiceController serviceController, IBusProvider provider)
    {
        _serviceController = serviceController;

        serviceController.SetOptionsEvent += async options => await provider.PublishAsync(options);
        serviceController.ReloadEvent += async serviceName => await provider.PublishAsync(new Reload
        {
            ServiceName = serviceName
        });
        
        provider
            .Subscribe<UpdatedServiceInfo, ServiceControllerWorker>(UpdatedServiceInfoHandle);
    }
    
    /// <summary>Метод, обрабатывающий сообщение об изменении состояния сервиса.</summary>
    /// <param name="serviceInfo">Объект состояния сервиса.</param>
    /// <param name="context">Контекст обработки сообщения.</param>
    /// <returns></returns>
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

/// <summary>
/// Класс, описывающий фоновый сервис, подписывающий на события <see cref="IServiceController{TEnvironment}"/><br/>
/// Унаследован от класса <see cref="ServiceControllerWorker"/>
/// </summary>
/// <typeparam name="TEnvironment">Тип окружения.</typeparam>
internal class ServiceControllerWorker<TEnvironment> : ServiceControllerWorker where TEnvironment : class, new()
{
    /// <summary>Инициализация сервиса.</summary>
    /// <param name="serviceController">Экземпляр контроллер сервисов.</param>
    /// <param name="provider">Экземпляр провайдера шины данных.</param>
    public ServiceControllerWorker(IServiceController<TEnvironment> serviceController, IBusProvider provider)
        : base(serviceController, provider)
    {
        serviceController.SetEnvironmentEvent += async environment =>
        {
            await provider.PublishAsync(environment);
        };
    }
}
