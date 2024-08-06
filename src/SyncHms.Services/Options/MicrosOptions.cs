namespace SyncHms.Services.Options;

public class MicrosOptions
{
    public const string Section = "Micros";

    [Required(AllowEmptyStrings = true)]
    public string Endpoint { get; set; } = string.Empty;

    public override int GetHashCode() => HashCode.Combine(Endpoint);

    public override bool Equals(object? obj) => 
        obj is MicrosOptions other && Endpoint == other.Endpoint;
}
