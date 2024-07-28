namespace SyncHms.Services;

public interface IFiscalService : IService<CheckDbOptions, ApplicationEnvironment>
{
    ApplicationEnvironment Environment { get; }

    void Exec(Action<CheckDBClient> action);

    T Exec<T>(Func<CheckDBClient, T> func);
}
