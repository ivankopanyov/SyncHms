namespace SyncHms.Bus.Events;

/// <summary>
/// Метод, обрабатывающий изменение даты и времени последней удачной обработки планируемого события.
/// </summary>
/// <param name="scheduleName">Имя планируемого события.</param>
/// <param name="options">Экземпляр опций планируемого события.</param>
public delegate Task UpdateScheduleHandleAsync(string scheduleName, ScheduleOptions options);

/// <summary>
/// Интерфейс, определяющий методы планировщика событий.<br/>
/// Расширяет методы интерфейса <see cref="IJob"/>
/// </summary>
public interface IEventScheduler : IJob
{
    /// <summary>Коллекция опций планируемых событий с именем обработчика в качестве ключа.</summary>
    IReadOnlyDictionary<string, ScheduleOptions> Schedules { get; }

    /// <summary>Событие, вызываемое при обновлении опций планируемого события.</summary>
    event UpdateScheduleHandleAsync UpdateScheduleEvent;

    /// <summary>Метод обновления опций планируемого события.</summary>
    /// <param name="scheduleName">Уникальное имя планируемого события.</param>
    /// <param name="interval">
    /// Интервал обработки планируемого события.<br/>
    /// Если передан <c>0</c> - событие будет остановлено.<br/>
    /// Если передан <c>null</c> - интервал не будет обновлен.<br/>
    /// </param>
    /// <param name="last">
    /// Дата и время последней удачной обработки события.<br/>
    /// Если передан <c>null</c> - дата не будет обновлена.<br/>
    /// </param>
    /// <param name="notify">
    /// Флаг, указывающий, нужно ли вызывать событие <see cref="IEventScheduler.UpdateScheduleEvent"/>
    /// </param>
    /// <returns>Экземпляр обновленных опций планируемого события.</returns>
    Task<ScheduleOptions> UpdateScheduleAsync(string scheduleName, TimeSpan? interval = null, DateTime? last = null, bool notify = false);
}
