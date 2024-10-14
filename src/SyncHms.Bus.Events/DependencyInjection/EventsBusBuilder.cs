namespace SyncHms.Bus.Events;

/// <summary>
/// Класс, описывающий построитель обработчиков событий из шины данных.
/// Реализует интерфейс <see cref="IEventsBusBuilder"/>
/// </summary>
/// <param name="services">Коллекция зависимостей приложения.</param>
public class EventsBusBuilder(IServiceCollection services) : ApplicationBuilder(services), IEventsBusBuilder
{
    /// <summary>Коллекция зависимостей приложения.</summary>
    private readonly IServiceCollection _services = services;

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
    public IEventsBusBuilder AddUnloggedEvent<THandler, TIn>() where THandler : Handler<TIn>
    {
        AddHandler(new HandlerOptions<THandler, TIn>());
        return this;
    }

    /// <summary>Метод, регистрирующий обработчик событий в контейнере зависимостей.</summary>
    /// <typeparam name="THandler">Тип обработчика.</typeparam>
    /// <typeparam name="TIn">Тип обрабатываемого сообщения.</typeparam>
    /// <param name="options">Инициализация опций обработчика.</param>
    private void AddHandler<THandler, TIn>(HandlerOptions<THandler, TIn> options) where THandler : HandlerBase<TIn>
    {
        _services
            .AddTransient<THandler>()
            .AddSingleton<IEventPublisher<TIn>, EventPublisher<TIn>>()
            .AddSingleton(options)
            .AddHostedService<HandlerWorker<THandler, TIn>>();
    }
}
