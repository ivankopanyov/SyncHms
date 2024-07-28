namespace SyncHms.Bus.Events;

public class Event
{
    [JsonProperty]
    internal string TaskId { get; set; }

    [JsonProperty]
    internal string? TaskName { get; set; }

    [JsonProperty]
    internal string? Error { get; set; }

    [JsonProperty]
    internal string? StackTrace { get; set; }

    internal Event()
    {
    }

    internal virtual async Task PublishAsync(IBusProvider provider)
    {
        await provider.PublishAsync(this);
    }
}

public class Event<T> : Event
{
    [JsonProperty]
    internal T Message { get; set; }

    internal Event()
    {
    }

    internal override async Task PublishAsync(IBusProvider provider)
    {
        await provider.PublishAsync(this);
    }
}
