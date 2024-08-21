namespace SyncHms.Bus.Events;

/// <summary>Базовый класс, описывающий обработчик события типа <see cref="EventLog"/>.</summary>
public abstract class LogHandler : HandlerBase<EventLog>
{
    /// <summary>Вызывает метод <see cref="HandleAsync"/></summary>
    /// <param name="in">Объект обрабатываемого события.</param>
    /// <param name="context">Объект контекста обработки события.</param>
    internal sealed override async Task ProcessHandleAsync(EventLog @in, IEventContext context)
    {
        await HandleAsync(@in);
    }

    /// <summary>Абстрактный метод, который должен содержать логику обработки события типа <see cref="EventLog"/>.</summary>
    /// <param name="in">Объект обрабатываемого события.</param>
    protected abstract Task HandleAsync(EventLog @in);
}
