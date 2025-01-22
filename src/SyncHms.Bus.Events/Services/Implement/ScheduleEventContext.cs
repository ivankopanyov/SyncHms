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

    /// <summary>
    /// Сообщение, полученное из метода <see cref="Handler{TIn}.Message"/><br/>
    /// Возвращает значение свойства <see cref="IEventContext.Message"/>
    /// </summary>
    public string? Message => baseContext.Message;

    /// <summary>
    /// Флаг, указывающий, будет ли логироваться удачная обработка события в случае отсутсвия ошибок.<br/>
    /// Игнорируется в случае добавления обрарботчика в контейнер зависимостей с помощью методов
    /// <see cref="IEventsBusBuilder.AddUnloggedEvent{THandler, TIn}"/> и <see cref="IEventsBusBuilder.AddScheduleEvent{THandler}"/><br/>
    /// Возвращает и изменяет значение свойства <see cref="IEventContext.Logiable"/>
    /// </summary>
    public bool Logiable
    {
        get => baseContext.Logiable;
        set => baseContext.Logiable = value;
    }

    /// <summary>
    /// Флаг, указывающий, была ли завершена предыдущая обработка текущего события с ошибкой.<br/>
    /// Возвращает значение свойства <see cref="IEventContext.HasError"/>
    /// </summary>
    public bool HasError => baseContext.HasError;

    /// <summary>Дата и время последней удачной обработки события.</summary>
    public DateTime Previous { get; init; }

    /// <summary>Дата и время текущей обработки события.</summary>
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

    /// <summary>Вызывает метод <see cref="IEventContext.SetHandlerName"/></summary>
    /// <param name="handlerName">Новое имя обработчика.</param>
    public void SetHandlerName(string handlerName)
    {
        baseContext.SetHandlerName(handlerName);
    }

    /// <summary>Вызывает метод <see cref="IEventContext.SetMessage"/></summary>
    /// <param name="message">Новое сообщение обработчика.</param>
    public void SetMessage(string message)
    {
        baseContext.SetMessage(message);
    }

    /// <summary>Вызывает метод <see cref="IEventContext.MarkWithError"/></summary>
    /// <param name="errorMessage">Сообщение ошибки.</param>
    /// <param name="stackTrace"></param>
    public void MarkWithError(string errorMessage, string? stackTrace = null)
    {
        baseContext.MarkWithError(errorMessage, stackTrace);
    }
}
