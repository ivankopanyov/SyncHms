namespace SyncHms.Events.UnitTests.Mocks;

public class MockEventContext : IScheduleEventContext
{
    private readonly List<object?> _sendMessages = [];

    private readonly List<(string?, Exception?)> _breaks = [];

    public string HandlerName { get; private set; } = "TEST";

    public string? Message { get; private set; }
    
    public bool Logiable { get; set; }

    public bool HasError { get; }
    
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

    public void SetHandlerName(string handlerName)
    {
        if (!string.IsNullOrWhiteSpace(handlerName))
            HandlerName = handlerName;
    }

    public void SetMessage(string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
            Message = message;
    }
    
    public void MarkWithError(string errorMessage, string? stackTrace = null)
    {
    }
}