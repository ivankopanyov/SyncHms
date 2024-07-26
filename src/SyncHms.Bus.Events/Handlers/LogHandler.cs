namespace SyncHms.Bus.Events;

public abstract class LogHandler : HandlerBase<EventLog>
{
    internal sealed override async Task ProcessHandleAsync(EventLog @in, IEventContext context)
    {
        await HandleAsync(@in);
    }

    protected abstract Task HandleAsync(EventLog @in);
}
