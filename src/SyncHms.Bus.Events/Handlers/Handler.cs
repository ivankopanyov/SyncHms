namespace SyncHms.Bus.Events;

/// <summary>Базовый класс, описывающий обработчик события типа <see cref="TIn"/>.</summary>
/// <typeparam name="TIn">Тип обрабатываемого события.</typeparam>
public abstract class Handler<TIn> : HandlerBase<TIn>
{
    /// <summary>Вызывает метод <see cref="HandleAsync"/></summary>
    /// <param name="in">Объект обрабатываемого события.</param>
    /// <param name="context">Объект контекста обработки события.</param>
    internal sealed override async Task ProcessHandleAsync(TIn @in, IEventContext context)
    {
        await HandleAsync(@in, context);
    }

    /// <summary>Абстрактный метод, который должен содержать логику обработки события.</summary>
    /// <param name="in">Объект обрабатываемого события.</param>
    /// <param name="context">Объект контекста обработки события.</param>
    protected abstract Task HandleAsync(TIn @in, IEventContext context);
}
