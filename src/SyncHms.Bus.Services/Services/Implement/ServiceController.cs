namespace SyncHms.Bus.Services.Services.Implement;

internal class ServiceController : IServiceController
{
    public event ChangedOptionsHandle? ChangedOptionsEvent;

    public event SetOptionsHandleAsync? SetOptionsEvent;

    public event ReloadHandleAsync? ReloadEvent;

    public void ChangedOptions(UpdatedServiceInfo serviceInfo) => ChangedOptionsEvent?.Invoke(serviceInfo);

    public void SetOptions(Options options) => SetOptionsEvent?.Invoke(options);

    public void Reload(string serviceName) => ReloadEvent?.Invoke(serviceName);
}

internal class ServiceController<TEnvironment> : ServiceController, IServiceController<TEnvironment>
    where TEnvironment : class, new()
{
    public event SetEnvironmentHandle<TEnvironment>? SetEnvironmentEvent;

    public void SetEnvironment(TEnvironment environment) => SetEnvironmentEvent?.Invoke(environment);
}
