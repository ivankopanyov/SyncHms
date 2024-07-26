namespace SyncHms.Bus.DependencyInjection;

internal class BusBuilder(IServiceCollection services) : IBusBuilder
{
    public IServiceCollection Services => services;
}
