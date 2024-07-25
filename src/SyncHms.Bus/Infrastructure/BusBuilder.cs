namespace SyncHms.Bus;

public class BusBuilder
{
    public IServiceCollection Services { get; private init; }

    internal BusBuilder(IServiceCollection services)
    {
        Services = services;
    }
}