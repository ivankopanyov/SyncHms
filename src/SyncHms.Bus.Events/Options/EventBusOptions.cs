namespace SyncHms.Bus.Events;

public class EventBusOptions
{
    public LoggerConfiguration? LoggerConfiguration { get; set; }

    public string? LogFileName { get; set; }
}
