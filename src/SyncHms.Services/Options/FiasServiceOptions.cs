namespace SyncHms.Services.Options;

public class FiasServiceOptions
{
    public const string Section = "Fias";

    public bool Enabled { get; set; }

    [Required(AllowEmptyStrings = true)]
    public string Host { get; set; } = string.Empty;

    [Range(IPEndPoint.MinPort, IPEndPoint.MaxPort)]
    public int Port { get; set; }

    public override int GetHashCode() => HashCode.Combine(Enabled, Host, Port);

    public override bool Equals(object? obj) =>
        obj is FiasServiceOptions other
        && Enabled == other.Enabled
        && Host == other.Host
        && Port == other.Port;
}