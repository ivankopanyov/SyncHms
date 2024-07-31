namespace SyncHms.Services;

public class FiasServiceOptions
{
    public bool Enabled { get; set; }
    
    [Required(AllowEmptyStrings = true)]
    public string Host { get; set; } = string.Empty;

    [Range(IPEndPoint.MinPort, IPEndPoint.MaxPort)]
    public int Port { get; set; }

    [Range(0, int.MaxValue), Description("Время ожидания в секундах при попытке установления подключения.")]
    public int ConnectionTimeout { get; set; } = 1;

    [Range(0, int.MaxValue), Description("Время ожидания в секундах перед попыткой установления подключения.")]
    public int ConnectionDelay { get; set; } = 6;

    public override int GetHashCode() => HashCode.Combine(Enabled, Host, Port);

    public override bool Equals(object? obj) =>
        obj is FiasServiceOptions other
        && Enabled == other.Enabled
        && Host == other.Host
        && Port == other.Port;
}