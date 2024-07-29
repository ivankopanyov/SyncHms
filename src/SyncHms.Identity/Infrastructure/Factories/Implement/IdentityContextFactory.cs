namespace SyncHms.Identity.Infrastructure.Factories.Implement;

internal class IdentityContextFactory<TContext> : IIdentityContextFactory where TContext : IdentityContext
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public IdentityContextFactory(IServiceScopeFactory serviceScopeFactory, IdentityOptions options)
    {
        _serviceScopeFactory = serviceScopeFactory;
        using var context = Create();
        if (options.UseMigrations)
            context.Database.Migrate();
        else
            context.Database.EnsureCreated();
    }
    
    public IdentityContext Create()
    {
        var serviceScope = _serviceScopeFactory.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<TContext>();
        context.ServiceScope = serviceScope;
        return context;
    }
}