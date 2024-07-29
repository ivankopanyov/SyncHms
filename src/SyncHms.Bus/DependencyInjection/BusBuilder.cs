namespace SyncHms.Bus.DependencyInjection;

internal class BusBuilder(IServiceCollection services) : ApplicationBuilder(services), IBusBuilder
{
}
