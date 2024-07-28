namespace SyncHms.Bus.Services;

public class ServiceInfo : Options
{
    public string Name { get; set; }

    public ServiceState? State { get; set; }
}
