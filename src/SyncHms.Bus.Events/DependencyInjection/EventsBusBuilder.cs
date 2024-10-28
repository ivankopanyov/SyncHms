namespace SyncHms.Bus.Events;

/// <summary>
/// Класс, описывающий построитель обработчиков событий из шины данных.
/// Реализует интерфейс <see cref="IEventsBusBuilder"/>
/// </summary>
public class EventsBusBuilder : ApplicationBuilder, IEventsBusBuilder
{
    private readonly EventSchedulerOptions _eventSheduleOptions = new();

    /// <summary>Коллекция зависимостей приложения.</summary>
    private readonly IServiceCollection _services;

    /// <summary>Инициализация объекта обработчика.</summary>
    /// <param name="services">Коллекция зависимостей приложения.</param>
    public EventsBusBuilder(IServiceCollection services) : base(services)
    {
        _services = services;
        _services
            .AddSingleton<IEventScheduler, EventScheduler>()
            .AddSingleton<IEventPublisher<ScheduleEvent>, EventPublisher<ScheduleEvent>>()
            .AddSingleton(_eventSheduleOptions)
            .AddQuartz()
            .AddQuartzHostedService();
    }

    /// <summary>Метод, регистрирующий обработчик событий в контейнере зависимостей.</summary>
    /// <typeparam name="THandler">Тип обработчика.</typeparam>
    /// <typeparam name="TIn">Тип обрабатываемого сообщения.</typeparam>
    /// <param name="action">Инициализация опций обработчика.</param>
    public IEventsBusBuilder AddEvent<THandler, TIn>(Action<HandlerOptions> action) where THandler : Handler<TIn>
    {
        ArgumentNullException.ThrowIfNull(action);
        var options = new HandlerOptions<THandler, TIn>()
        {
            UseEventLogging = true
        };
        action.Invoke(options);

        AddHandler(options);
        return this;
    }

    /// <summary>Метод, регистрирующий обработчик логов в контейнере зависимостей.</summary>
    /// <typeparam name="THandler">Тип обработчика. Должен быть унаследован от класса <see cref="LogHandler"/></typeparam>
    public IEventsBusBuilder AddEventLog<THandler>() where THandler : LogHandler
    {
        AddHandler(new HandlerOptions<THandler, EventLog>());
        return this;
    }

    /// <summary>Метод, регистрирующий обработчик нелогируемых событий в контейнере зависимостей.</summary>
    /// <typeparam name="THandler">Тип обработчика.</typeparam>
    /// <typeparam name="TIn">Тип обрабатываемого сообщения.</typeparam>
    /// <param name="action">Инициализация опций обработчика.</param>
    public IEventsBusBuilder AddUnloggedEvent<THandler, TIn>(Action<HandlerOptions>? action = null) where THandler : Handler<TIn>
    {
        var options = new HandlerOptions<THandler, TIn>();
        action?.Invoke(options);

        AddHandler(options);
        return this;
    }

    /// <summary>Метод, регистрирующий обработчик событий, отправляемыx планировщиком <see cref="IEventScheduler"/></summary>
    /// <typeparam name="THandler">Тип обработчика.</typeparam>
    /// <param name="action">Инициализация опций обработчика.</param>
    /// <exception cref="ArgumentException">
    /// Исключение возбуждается, если имя обработчика не инициализировано или уже зарегистрировано.
    /// </exception>
    public IEventsBusBuilder AddScheduleEvent<THandler>(Action<HandlerOptions> action) where THandler : ScheduleHandler
    {
        ArgumentNullException.ThrowIfNull(action);
        var options = new HandlerOptions<THandler, ScheduleEvent>();
        action.Invoke(options);

        ArgumentException.ThrowIfNullOrWhiteSpace(options.HandlerName, nameof(options.HandlerName));

        var description = typeof(THandler).GetCustomAttribute<ScheduleDescriptionAttribute>()?.Description;
        if (!_eventSheduleOptions.Events.TryAdd(options.HandlerName, new(options.HandlerName, description)))
            throw new ArgumentException($"ScheduleHandler named {options.HandlerName} is already registered.");

        AddHandler(options, false);
        return this;
    }

    /// <summary>Метод, регистрирующий обработчик событий в контейнере зависимостей.</summary>
    /// <typeparam name="THandler">Тип обработчика.</typeparam>
    /// <typeparam name="TIn">Тип обрабатываемого сообщения.</typeparam>
    /// <param name="options">Инициализация опций обработчика.</param>
    /// <param name="addPublisher">
    /// Флаг, указывающий, нужно ли регистрировать объект типа
    /// <see cref="IEventPublisher{TIn}"/> в контейнере зависимостей.
    /// </param>
    private void AddHandler<THandler, TIn>(HandlerOptions<THandler, TIn> options, bool addPublisher = true) where THandler : HandlerBase<TIn>
    {
        _services
            .AddTransient<THandler>()
            .AddSingleton(options)
            .AddHostedService<HandlerWorker<THandler, TIn>>();

        if (addPublisher)
            _services
                .AddSingleton<IEventPublisher<TIn>, EventPublisher<TIn>>();
    }
}
