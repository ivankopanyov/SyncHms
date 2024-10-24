namespace SyncHms.Bus.Events;

/// <summary>Базовый класс, описывающий обработчик события типа <see cref="TIn"/>.</summary>
/// <typeparam name="TIn">Тип обрабатываемого события.</typeparam>
public abstract class HandlerBase<TIn>
{
    /// <summary>Защищенный метод инициализации базового класса обработчика события.</summary>
    protected internal HandlerBase()
    {
    }

    /// <summary>Абстрактный метод, который должен содержать логику обработки события.</summary>
    /// <param name="in">Объект обрабатываемого события.</param>
    /// <param name="context">Объект контекста обработки события.</param>
    internal abstract Task ProcessHandleAsync(TIn @in, IEventContext context);

    /// <summary>Метод переопределения стандартного имени обработчика.</summary>
    /// <param name="in">Объект обрабатываемого события.</param>
    /// <returns>
    /// Переопределленое имя обработчика.<br/>
    /// Если будет возвращен <c>null</c> - имя обарбочика не будет переопределено.<br/>
    /// По умолчанию <c>null</c>.
    /// </returns>
    protected internal virtual string? HandlerName(TIn @in) => null;

    /// <summary>Метод, инициализирующий краткое описание события.</summary>
    /// <param name="in">Объект обрабатываемого события.</param>
    /// <returns>Краткое описание события. По умолчанию <c>null</c>.</returns>
    protected internal virtual string? Message(TIn @in) => null;
}
