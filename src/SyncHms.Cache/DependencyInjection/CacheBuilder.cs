namespace SyncHms.Cache.DependencyInjection;

/// <summary>
/// Класс, описывающий построитель сервисов приложения.
/// Реализует интерфейс <see cref="ICacheBuilder"/>
/// </summary>
/// <param name="services">Коллекция зависимостей приложения.</param>
internal class CacheBuilder(IServiceCollection services) : ApplicationBuilder(services), ICacheBuilder
{
}
