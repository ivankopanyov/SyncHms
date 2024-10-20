namespace SyncHms.Bus.Events;

/// <summary>Базовый класс, описывающий обработчик планируемого события.</summary>
public abstract class ScheduleHandler : HandlerBase<ScheduleEvent>
{
    /// <summary>Вызывает метод <see cref="HandleAsync"/></summary>
    /// <param name="in">Объект обрабатываемого события.</param>
    /// <param name="context">Объект контекста обработки события.</param>
    internal sealed override async Task ProcessHandleAsync(ScheduleEvent @in, IEventContext context)
    {
        if (context.HandlerName != @in.Destination)
            return;

        var eventContext = new ScheduleEventContext(context)
        {
            Previous = @in.Previous,
            Current = @in.Current
        };

        try
        {
            await HandleAsync(eventContext);
            await @in.EventScheduler.UpdateScheduleAsync(context.HandlerName, @in.Previous, @in.Current);
        }
        catch (Exception ex)
        {
            await @in.EventScheduler.UpdateScheduleAsync(context.HandlerName, @in.Previous, @in.Current, ex);

            if (ex is not TaskCriticalException)
                context.Break(ex.Message, ex);
        }
    }

    /// <summary>Абстрактный метод, который должен содержать логику обработки события.</summary>
    /// <param name="context">Объект контекста обработки события.</param>
    protected abstract Task HandleAsync(IScheduleEventContext context);
}
