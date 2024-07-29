namespace SyncHms.Server.Options;

public class IdentityProviderOptions
{
    public const string Section = "Identity";
    
    public IdentityProvider Provider { get; set; }
    
    public SqlProviderOptions? Sql { get; set; }
}
