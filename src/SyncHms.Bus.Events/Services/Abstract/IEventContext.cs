namespace SyncHms.Bus.Events;

/// <summary>Интерфейс, определяющий методы контекста обработки события.</summary>
public interface IEventContext
{
    /// <summary>Имя текущего обработчика.</summary>
    string HandlerName { get; }

    string? Message { get; }

    bool Logiable { get; set; }

    public bool HasError { get; }

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

    void SetHandlerName(string handlerName);

    void SetMessage(string message);
}
