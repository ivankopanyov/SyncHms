namespace SyncHms.Bus.Events;

public class EventLog
{
    public string TaskId { get; set; }

    public string? TaskName { get; set; }

    public string? HandlerName { get; set; }
    
    public DateTime DateTime { get; set; } = DateTime.Now.Trim(TimeSpan.TicksPerMillisecond);

    public bool IsEnd { get; set; }

    public string? Message { get; set; }
    
    public string? Error { get; set; }

    public string? StackTrace { get; set; }

    public string? InputObjectJson { get; set; }
}
