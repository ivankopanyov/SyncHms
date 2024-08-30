namespace SyncHms.Bus.DependencyInjection;

/// <summary>
/// Класс, описывающий построитель шины данных.
/// Реализует интерфейс <see cref="IBusBuilder"/>.
/// </summary>
/// <param name="services">Коллекция сервисов приложения.</param>
internal class BusBuilder(IServiceCollection services) : ApplicationBuilder(services), IBusBuilder
{
}
