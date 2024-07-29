using Newtonsoft.Json.Serialization;

namespace SyncHms.Domain.EntityFramework;

public class EntityFrameworkDomainOptions
{
    internal JsonSerializerSettings JsonSerializerSettings { get; } = new();
    
    public bool UseMigrations { get; set; } = true;

    public IContractResolver ContractResolver
    {
        get => JsonSerializerSettings.ContractResolver ??= new DefaultContractResolver();
        set => JsonSerializerSettings.ContractResolver = value ?? new DefaultContractResolver();
    }
}
