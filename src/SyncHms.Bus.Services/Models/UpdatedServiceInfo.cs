namespace SyncHms.Bus.Services;

public class UpdatedServiceInfo : ServiceInfo
{
    public bool UpdateOptions { get; set; } = true;
    
    public bool ResponseRequired { get; set; }
}
