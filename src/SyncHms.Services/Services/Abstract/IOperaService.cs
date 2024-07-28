namespace SyncHms.Services;

public interface IOperaService : IService<OperaOptions, ApplicationEnvironment>
{
    ApplicationEnvironment Environment { get; }

    void Exec(Action<OperaDbContext> action);

    T Exec<T>(Func<OperaDbContext, T> func);
}
