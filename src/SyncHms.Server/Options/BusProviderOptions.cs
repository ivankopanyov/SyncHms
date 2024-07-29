namespace SyncHms.Server.Options;

public class BusProviderOptions
{
    public const string Section = "Bus";
    
    public BusProvider Provider { get; set; }
    
    public SqlProviderOptions? Sql { get; set; }
}
