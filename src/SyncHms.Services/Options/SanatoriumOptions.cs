namespace SyncHms.Services.Options;

public class SanatoriumOptions
{
    public const string Section = "Sanatorium";

    public bool Enabled { get; set; }
    
    [Required(AllowEmptyStrings = true)]
    public string ConnectionString { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = true)]
    public string Endpoint { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = true)]
    public string License { get; set; } = string.Empty;

    public override int GetHashCode() =>
        HashCode.Combine(
            Enabled,
            ConnectionString,
            Endpoint,
            License);

    public override bool Equals(object? obj) => 
        obj is SanatoriumOptions other
        && Enabled == other.Enabled
        && ConnectionString == other.ConnectionString
        && Endpoint == other.Endpoint
        && License == other.License;
}
