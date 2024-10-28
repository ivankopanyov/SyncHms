namespace SyncHms.Bus.Events.Services.Implement;

/// <summary>
/// Класс, описывающий контекст обработки планируемого события.<br/>
/// Реализует интерфейс <see cref="IScheduleEventContext"/>
/// </summary>
/// <param name="baseContext">Экземпляр базового контекста обработки события.</param>
internal class ScheduleEventContext(IEventContext baseContext) : IScheduleEventContext
{
    /// <summary>
    /// Имя текущего обработчика.<br/>
    /// Возвращает значение свойства <see cref="IEventContext.HandlerName"/>
    /// </summary>
    public string HandlerName => baseContext.HandlerName;

    public DateTime Previous { get; init; }

    public DateTime Current { get; init; }

    /// <summary>Флаг, укзывающий, что нужно обновить идентификатор отправляемых сообщений.</summary>
    public bool UpdateTaskId
    {
        get => baseContext.UpdateTaskId;
        set => baseContext.UpdateTaskId = value;
    }

    /// <summary>
    /// Метод, добавляющий событие для публикации в шину данных.<br/>
    /// Вызывает метод <see cref="IEventContext.Send"/>
    /// </summary>
    /// <typeparam name="TIn">Тип сообщения.</typeparam>
    /// <param name="in">Экземпляр сообщения.</param>
    public void Send<TIn>(TIn @in)
    {
        baseContext.Send(@in);
    }

    /// <summary>
    /// Метод возбуждает исключение <see cref="TaskCriticalException"/>,<br/>
    /// что прерывает процесс обработки и исключает возвращение события обратно в очередь.<br/>
    /// Вызывает метод <see cref="IEventContext.Break"/>
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    /// <param name="innerException">Внутреннее исключение процесса обработки события.</param>
    public void Break(string? message = null, Exception? innerException = null)
    {
        baseContext.Break(message, innerException);
    }
}
