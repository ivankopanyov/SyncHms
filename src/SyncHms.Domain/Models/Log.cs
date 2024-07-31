namespace SyncHms.Domain;

public class Log : IComparable<Log>
{
    public string Id { get; set; }
    
    [JsonIgnore]
    public string LogDataId { get; set; }

    public string TaskId { get; set; }

    public string? TaskName { get; set; }

    public string? HandlerName { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime DateTime { get; set; }

    public bool IsError { get; set; }

    public bool IsEnd { get; set; }

    public string? Message { get; set; }

    public virtual LogData LogData { get; set; }

    public int CompareTo(Log? other) => -DateTime.Compare(DateTime, other?.DateTime ?? DateTime.MinValue);
}
