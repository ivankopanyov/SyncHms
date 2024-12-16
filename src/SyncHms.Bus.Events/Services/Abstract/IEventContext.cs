namespace SyncHms.Bus.Events;

/// <summary>Интерфейс, определяющий методы контекста обработки события.</summary>
public interface IEventContext
{
    /// <summary>Имя текущего обработчика.</summary>
    string HandlerName { get; }

    /// <summary>Сообщение, полученное из метода <see cref="Handler{TIn}.Message"/></summary>
    string? Message { get; }

    /// <summary>
    /// Флаг, указывающий, будет ли логироваться удачная обработка события в случае отсутсвия ошибок.<br/>
    /// Игнорируется в случае добавления обрарботчика в контейнер зависимостей с помощью методов
    /// <see cref="IEventsBusBuilder.AddUnloggedEvent{THandler, TIn}"/> и <see cref="IEventsBusBuilder.AddScheduleEvent{THandler}"/>
    /// </summary>
    bool Logiable { get; set; }

    /// <summary>Флаг, указывающий, была ли завершена предыдущая обработка текущего события с ошибкой.</summary>
    bool HasError { get; }

    /// <summary>Флаг, укзывающий, что нужно обновить идентификатор отправляемых сообщений.</summary>
    internal bool UpdateTaskId { get; set; }

    /// <summary>Метод, добавляющий событие для публикации в шину данных.</summary>
    /// <typeparam name="TIn">Тип сообщения.</typeparam>
    /// <param name="in">Экземпляр сообщения.</param>
    void Send<TIn>(TIn @in);

    /// <summary>
    /// Метод возбуждает исключение <see cref="TaskCriticalException"/>,<br/>
    /// что прерывает процесс обработки и исключает возвращение события обратно в очередь.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    /// <param name="innerException">Внутреннее исключение процесса обработки события.</param>
    void Break(string? message = null, Exception? innerException = null);

    /// <summary>Переопределяет имя обработчика в логах, если переданный параметр не пустой и не <c>null</c></summary>
    /// <param name="handlerName">Новое имя обработчика.</param>
    void SetHandlerName(string handlerName);

    /// <summary>Переопределяет сообщение обработчика в логах, если переданный параметр не пустой и не <c>null</c></summary>
    /// <param name="message">Новое сообщение обработчика.</param>
    void SetMessage(string message);
}
