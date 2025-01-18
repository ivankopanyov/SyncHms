namespace SyncHms.Bus.Events.Services.Implement;

/// <summary>
/// Класс, описывающий контекст обработки события.
/// Реализует интерфейс <see cref="IEventContext"/>
/// </summary>
/// <param name="handlerName">Имя текущего обработчика.</param>
/// <param name="message">Сообщение текущего обработчика.</param>
/// <param name="hasError">Флаг, указывающий, была ли завершена предыдущая обработка текущего события с ошибкой.</param>
internal class EventContext(string handlerName, string? message, bool hasError) : IEventContext
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
    public string HandlerName { get; private set; } = handlerName;

    /// <summary>Сообщение, полученное из метода <see cref="Handler{TIn}.Message"/></summary>
    public string? Message { get; private set; } = message;

    /// <summary>
    /// Флаг, указывающий, будет ли логироваться удачная обработка события в случае отсутсвия ошибок.<br/>
    /// Игнорируется в случае добавления обрарботчика в контейнер зависимостей с помощью методов
    /// <see cref="IEventsBusBuilder.AddUnloggedEvent{THandler, TIn}"/> и <see cref="IEventsBusBuilder.AddScheduleEvent{THandler}"/>
    /// </summary>
    public bool Logiable { get; set; } = true;

    /// <summary>Флаг, указывающий, была ли завершена предыдущая обработка текущего события с ошибкой.</summary>
    public bool HasError { get; set; } = hasError;

    public string? ErrorMessage { get; set; }

    public string? StackTrace { get; set; }

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

    /// <summary>Переопределяет имя обработчика в логах, если переданный параметр не пустой и не <c>null</c></summary>
    /// <param name="handlerName">Новое имя обработчика.</param>
    public void SetHandlerName(string handlerName)
    {
        if (!string.IsNullOrWhiteSpace(handlerName))
            HandlerName = handlerName;
    }

    /// <summary>Переопределяет сообщение обработчика в логах, если переданный параметр не пустой и не <c>null</c></summary>
    /// <param name="message">Новое сообщение обработчика.</param>
    public void SetMessage(string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
            Message = message;
    }

    /// <summary>
    /// Помечает сообщение в логах с ошибкой, но продожает выполнение задачи,
    /// если параметр <c>errorMessage</c> не пустой и не <c>null</c>
    /// </summary>
    /// <param name="errorMessage">Сообщение ошибки.</param>
    /// <param name="stackTrace"></param>
    public void MarkWithError(string errorMessage, string? stackTrace = null)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
            return;

        ErrorMessage = errorMessage;
        StackTrace = stackTrace;
    }
}
