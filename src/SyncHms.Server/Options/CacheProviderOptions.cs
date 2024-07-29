namespace SyncHms.Server.Options;

public class CacheProviderOptions
{
    public const string Section = "Cache";
    
    public CacheProvider Provider { get; set; }
}
