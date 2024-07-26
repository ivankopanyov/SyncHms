namespace SyncHms.Bus.Events;

public abstract class HandlerBase<TIn>
{
    protected internal HandlerBase()
    {
    }

    internal abstract Task ProcessHandleAsync(TIn @in, IEventContext context);

    protected internal virtual string? Message(TIn @in) => null;
}
