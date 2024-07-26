namespace SyncHms.Bus.Services;

public delegate void ChangedOptionsHandle(UpdatedServiceInfo serviceInfo);

internal delegate Task SetOptionsHandleAsync(Options options);

internal delegate Task ReloadHandleAsync(string serviceName);

internal delegate void SetEnvironmentHandle<in TEnvironment>(TEnvironment environment) where TEnvironment : class, new();

public interface IServiceController
{
    event ChangedOptionsHandle? ChangedOptionsEvent;

    internal event SetOptionsHandleAsync? SetOptionsEvent;

    internal event ReloadHandleAsync? ReloadEvent;

    internal void ChangedOptions(UpdatedServiceInfo serviceInfo);

    void SetOptions(Options options);

    void Reload(string serviceName);
}

public interface IServiceController<TEnvironment> : IServiceController where TEnvironment : class, new()
{
    internal event SetEnvironmentHandle<TEnvironment>? SetEnvironmentEvent;

    void SetEnvironment(TEnvironment environment);
}
