namespace SyncHms.Bus.Events;

/// <summary>
/// Интерфейс, содержащий методы для регистрации обработчиков событий из шины данных.
/// Расширяет интерфейс <see cref="IBusBuilder"/>
/// </summary>
public interface IEventsBusBuilder : IBusBuilder
{
    /// <summary>Метод, регистрирующий обработчик событий в контейнере зависимостей.</summary>
    /// <typeparam name="THandler">Тип обработчика.</typeparam>
    /// <typeparam name="TIn">Тип обрабатываемого сообщения.</typeparam>
    /// <param name="action">Инициализация опций обработчика.</param>
    IEventsBusBuilder AddEvent<THandler, TIn>(Action<HandlerOptions> action) where THandler : Handler<TIn>;

    /// <summary>Метод, регистрирующий обработчик логов в контейнере зависимостей.</summary>
    /// <typeparam name="THandler">Тип обработчика. Должен быть унаследован от класса <see cref="LogHandler"/></typeparam>
    IEventsBusBuilder AddEventLog<THandler>() where THandler : LogHandler;

    /// <summary>Метод, регистрирующий обработчик нелогируемых событий в контейнере зависимостей.</summary>
    /// <typeparam name="THandler">Тип обработчика.</typeparam>
    /// <typeparam name="TIn">Тип обрабатываемого сообщения.</typeparam>
    IEventsBusBuilder AddUnloggedEvent<THandler, TIn>() where THandler : Handler<TIn>;

    /// <summary>Метод, регистрирующий обработчик событий, отправляемыx планировщиком <see cref="IEventScheduler"/></summary>
    /// <typeparam name="THandler">Тип обработчика.</typeparam>
    /// <param name="action">Инициализация опций обработчика.</param>
    IEventsBusBuilder AddScheduleEvent<THandler>(Action<HandlerOptions> action) where THandler : ScheduleHandler;
}
