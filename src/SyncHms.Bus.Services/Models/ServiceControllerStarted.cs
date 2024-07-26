namespace SyncHms.Bus.Services;

public class ServiceControllerStarted
{
}

public class ServiceControllerStarted<TEnvironment> where TEnvironment : class, new()
{
    public TEnvironment Environment { get; set; }
}
