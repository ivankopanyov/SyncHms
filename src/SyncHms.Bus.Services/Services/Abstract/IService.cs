namespace SyncHms.Bus.Services;

public interface IService<TOptions> where TOptions : class, new()
{
    Task ChangedOptionsHandleAsync(TOptions options);
}

public interface IService<TOptions, TEnvironment> : IService<TOptions>
    where TOptions : class, new() where TEnvironment : class, new()
{
    TEnvironment Environment { get; }

    Task ChangedEnvironmentHandleAsync(TEnvironment current, TEnvironment previous);
}
