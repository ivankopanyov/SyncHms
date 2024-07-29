namespace SyncHms.Server.Options;

public class DomainProviderOptions
{
    public const string Section = "Domain";
    
    public DomainProvider Provider { get; set; }
    
    public SqlProviderOptions? Sql { get; set; }
}
