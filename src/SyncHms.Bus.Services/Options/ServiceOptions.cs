namespace SyncHms.Bus.Services;

public class ServiceOptions
{
    public string ServiceName { get; set; }
}

public class ServiceOptions<TOptions> : ServiceOptions where TOptions : class, new()
{
}
