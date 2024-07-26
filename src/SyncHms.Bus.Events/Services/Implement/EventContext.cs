namespace SyncHms.Bus.Events.Services.Implement;

internal class EventContext : IEventContext
{
    private readonly List<Event> _events = [];

    public IEnumerable<Event> Events => _events;

    public void Send<TIn>(TIn @in)
    {
        if (@in == null)
            return;
        
        _events.Add(new Event<TIn>
        {
            Message = @in
        });
    }

    public void Break(string? message = null, Exception? innerException = null) =>
        throw new TaskCriticalException(message, innerException);
}
