namespace SyncHms.Bus.Services;

public interface IService<in TOptions> where TOptions : class, new()
{
    Task ChangedOptionsHandleAsync(TOptions options);
}

public interface IService<in TOptions, in TEnvironment> : IService<TOptions>
    where TOptions : class, new() where TEnvironment : class, new()
{
    Task ChangedEnvironmentHandleAsync(TEnvironment current, TEnvironment previous);
}
