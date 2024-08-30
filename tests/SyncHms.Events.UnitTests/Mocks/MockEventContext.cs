namespace SyncHms.Events.UnitTests.Mocks;

public class MockEventContext : IEventContext
{
    private readonly List<object?> _sendMessages = [];

    private readonly List<(string?, Exception?)> _breaks = [];

    public IReadOnlyList<object?> SendMessages => _sendMessages;
    
    public IReadOnlyList<(string? Message, Exception? Exception)> Breaks => _breaks;
    
    public void Send<TIn>(TIn @in)
    {
        _sendMessages.Add(@in);
    }

    public void Break(string? message = null, Exception? innerException = null)
    {
        _breaks.Add((message, innerException));
    }
}