namespace SyncHms.Bus.Events;

public abstract class Handler<TIn> : HandlerBase<TIn>
{
    internal sealed override async Task ProcessHandleAsync(TIn @in, IEventContext context)
    {
        await HandleAsync(@in, context);
    }

    protected abstract Task HandleAsync(TIn @in, IEventContext context);
}
