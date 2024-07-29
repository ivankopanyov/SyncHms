namespace SyncHms.Bus.EntityFramework.Infrastructure.Factories.Implement;

internal class BusContextFactory<TContext> : IBusContextFactory where TContext : BusContext
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public BusContextFactory(IServiceScopeFactory serviceScopeFactory, EntityFrameworkBusOptions options)
    {
        _serviceScopeFactory = serviceScopeFactory;
        using var context = Create();
        if (options.UseMigrations)
            context.Database.Migrate();
        else
            context.Database.EnsureCreated();
    }
    
    public BusContext Create()
    {
        var serviceScope = _serviceScopeFactory.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<TContext>();
        context.ServiceScope = serviceScope;
        return context;
    } 
}
