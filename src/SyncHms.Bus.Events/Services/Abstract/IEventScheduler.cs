namespace SyncHms.Bus.Events;

/// <summary>
/// Метод, обрабатывающий изменение даты и времени последней удачной обработки планируемого события.
/// </summary>
/// <param name="handlerName">Имя обработчика планируемого события.</param>
/// <param name="last">Дата и время последней удачной обработки планируемого события.</param>
/// <param name="description">Описание планируемого события.</param>
/// <returns></returns>
public delegate Task UpdateLastDateHandleAsync(string handlerName, DateTime last, string? description);

/// <summary>
/// Интерфейс, определяющий методы планировщика событий.<br/>
/// Расширяет методы интерфейса <see cref="IJob"/>
/// </summary>
public interface IEventScheduler : IJob
{
    /// <summary>
    /// Событие, вызываемое при изменении даты и времени последней удачной обработки планируемого события.
    /// </summary>
    event UpdateLastDateHandleAsync UpdateLastDateEvent;

    /// <summary>Метод обновления опций планируемого события.</summary>
    /// <param name="handlerName">Уникальное имя обработчика планируемого события.</param>
    /// <param name="interval">
    /// Интервал обработки планируемого события.<br/>
    /// Если передан <c>0</c> - событие будет остановлено.<br/>
    /// Если передан <c>null</c> - интервал не будет обновлен.<br/>
    /// </param>
    /// <param name="last">
    /// Дата и время последней удачной обработки события.<br/>
    /// Если передан <c>null</c> - дата не будет обновлена.<br/>
    /// </param>
    /// <param name="lastDateNotify">
    /// Флаг, указывающий, нужно ли вызывать событие обновления даты и времени
    /// <see cref="IEventScheduler.UpdateLastDateEvent"/> последней удачной обработки планируемого события.
    /// </param>
    Task UpdateSheduleEventAsync(string handlerName, TimeSpan? interval = null, DateTime? last = null, bool lastDateNotify = false);
}
