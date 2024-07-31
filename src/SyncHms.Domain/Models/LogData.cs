namespace SyncHms.Domain;

public class LogData
{
    [JsonIgnore]
    public string Id { get; set; }
    
    [JsonIgnore]
    public string LogId { get; set; }
    
    public string? Error { get; set; }

    public string? StackTrace { get; set; }

    public string? InputObjectJson { get; set; }
    
    public virtual Log Log { get; set; }
}