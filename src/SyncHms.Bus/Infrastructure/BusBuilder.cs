namespace SyncHms.Bus;

public class BusBuilder(IServiceCollection services)
{
    public IServiceCollection Services { get; private init; } = services;
}
