namespace SyncHms.Bus.EntityFramework.Infrastructure.Factories.Implement;

internal class BusContextFactory(IServiceScopeFactory serviceScopeFactory) : IBusContextFactory
{
    public BusContext Create()
    {
        var serviceScope = serviceScopeFactory.CreateScope();
        return serviceScope.ServiceProvider.GetRequiredService<BusContext>();
    }
}