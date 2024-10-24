namespace SyncHms.Bus.Events.Services.Implement;

/// <summary>
/// Класс, описывающий контекст обработки события.
/// Реализует интерфейс <see cref="IEventContext"/>
/// </summary>
/// <param name="handlerName">Имя текущего обработчика.</param>
internal class EventContext(string handlerName) : IEventContext
{
    /// <summary>Список событий, которые должны быть опубликованы в шине данных.</summary>
    private readonly List<Event> _events = [];

    /// <summary>Флаг, укзывающий, что нужно обновить идентификатор отправляемых сообщений.</summary>
    public bool UpdateTaskId { get; set; }

    /// <summary>
    /// Свойство, возвращающее перечисление событий,
    /// которые должны быть опубликованы в шине данных.
    /// </summary>
    public IEnumerable<Event> Events => _events;

    /// <summary>Имя текущего обработчика.</summary>
    public string HandlerName => handlerName;

    /// <summary>Метод, добавляющий событие для публикации в шину данных.</summary>
    /// <typeparam name="TIn">Тип сообщения.</typeparam>
    /// <param name="in">Экземпляр сообщения.</param>
    public void Send<TIn>(TIn @in)
    {
        if (@in == null)
            return;
        
        _events.Add(new Event<TIn>
        {
            Message = @in
        });
    }

    /// <summary>
    /// Метод возбуждает исключение <see cref="TaskCriticalException"/>,<br/>
    /// что прерывает процесс обработки и исключает возвращение события обратно в очередь.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    /// <param name="innerException">Внутреннее исключение процесса обработки события.</param>
    public void Break(string? message = null, Exception? innerException = null) =>
        throw new TaskCriticalException(message, innerException);
}
