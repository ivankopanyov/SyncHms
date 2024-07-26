namespace SyncHms.Bus.Events;

public abstract class HandlerOptions
{
    public string TaskName { get; set; }

    public string HandlerName { get; set; }

    internal bool UseEventLogging { get; set; }
}

public class HandlerOptions<THandler, TIn>
    : HandlerOptions where THandler : HandlerBase<TIn>
{
}
