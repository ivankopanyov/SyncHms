namespace SyncHms.Services;

/// <summary>
/// Интерфейс, определяющий методы построителя сервисов приложения.<br/>
/// Расширяет методы интерфейса <see cref="IServicesBusBuilder{TEnvironment}"/>
/// </summary>
public interface IApplicationServicesBuilder : IServicesBusBuilder<ApplicationEnvironment>
{
}
