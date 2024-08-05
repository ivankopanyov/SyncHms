namespace SyncHms.Services;

public class CheckDbOptions
{
    [Required(AllowEmptyStrings = true)]
    public string Endpoint { get; set; } = string.Empty;

    public override int GetHashCode() => HashCode.Combine(Endpoint);

    public override bool Equals(object? obj) => 
        obj is CheckDbOptions other && Endpoint == other.Endpoint;
}
