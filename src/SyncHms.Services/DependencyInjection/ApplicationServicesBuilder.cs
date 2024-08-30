namespace SyncHms.Services.DependencyInjection;

/// <summary>
/// Класс, описывающий построитель сервисов приложения.<br/>
/// Унаследован от класса <see cref="ServicesBusBuilder{TEnvironment}"/><br/>
/// Реализует интерфейс <see cref="IServicesBusBuilder{TEnvironment}"/>
/// </summary>
/// <param name="services">Коллекция сервисов приложения.</param>
internal class ApplicationServicesBuilder(IServiceCollection services) :
    ServicesBusBuilder<ApplicationEnvironment>(services), IApplicationServicesBuilder
{
}
