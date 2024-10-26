namespace SyncHms.Events.UnitTests.Mocks;

public class MockEventContext : IScheduleEventContext
{
    private readonly List<object?> _sendMessages = [];

    private readonly List<(string?, Exception?)> _breaks = [];

    public string HandlerName => "TEST";

    public bool UpdateTaskId { get; set; }

    public IReadOnlyList<object?> SendMessages => _sendMessages;
    
    public IReadOnlyList<(string? Message, Exception? Exception)> Breaks => _breaks;

    public DateTime Previous => DateTime.Now;

    public DateTime Current => DateTime.Now;

    public void Send<TIn>(TIn @in)
    {
        _sendMessages.Add(@in);
    }

    public void Break(string? message = null, Exception? innerException = null)
    {
        _breaks.Add((message, innerException));
    }
}