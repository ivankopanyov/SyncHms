namespace SyncHms.Bus.Services;

public class ServicesSerializerSettings : JsonSerializerSettings
{
    public ServicesSerializerSettings()
    {
        ContractResolver = new DescriptionContractResolver();
    }
}
