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
            await UpdateLastAsync(@in.EventScheduler, context.HandlerName, @in.Current);
        }
        catch (Exception ex)
        {
            await UpdateLastAsync(@in.EventScheduler, context.HandlerName, @in.Previous);
            if (ex is not TaskCriticalException)
                context.Break(ex.Message, ex);
        }
    }

    /// <summary>Абстрактный метод, который должен содержать логику обработки события.</summary>
    /// <param name="context">Объект контекста обработки события.</param>
    protected abstract Task HandleAsync(IScheduleEventContext context);

    /// <summary>Метод, инициализирующий обновление даты и времени последней удачной обработки события.</summary>
    /// <param name="eventScheduler">Экземпляр планировщика событий.</param>
    /// <param name="scheduleName">Имя планируемого события.</param>
    /// <param name="dateTime">Дата и время последней удачной обработки события.</param>
    private static async Task UpdateLastAsync(IEventScheduler eventScheduler, string scheduleName, DateTime dateTime)
    {
        try
        {
            await eventScheduler.UpdateScheduleAsync(scheduleName, last: dateTime, notify: true);
        }
        catch 
        {
            // ignored
        }
    }
}
