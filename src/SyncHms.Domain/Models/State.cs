namespace SyncHms.Domain;

public class State
{
    public bool IsActive { get; set; }

    public string? Error { get; set; }

    public string? StackTrace { get; set; }

    public string? Info { get; set; }

    public State Clone() => (State)MemberwiseClone();
}