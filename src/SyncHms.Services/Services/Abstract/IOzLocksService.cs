namespace SyncHms.Services;

/// <summary>
/// Интерфейс, определяющий методы сервиса интеграции с базой данных <c>OzLocks</c><br/>
/// Расширяет методы интерфейса <see cref="IService{TOptions,TEnvironment}"/>
/// </summary>
public interface IOzLocksService : IService<OzLocksOptions, ApplicationEnvironment>
{
}
