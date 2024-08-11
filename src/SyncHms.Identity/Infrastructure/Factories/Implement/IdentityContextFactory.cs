namespace SyncHms.Identity.Infrastructure.Factories.Implement;

internal class IdentityContextFactory<TContext>(IServiceScopeFactory serviceScopeFactory)
    : IIdentityContextFactory where TContext : IdentityContext
{
    public IdentityContext Create()
    {
        var serviceScope = serviceScopeFactory.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<TContext>();
        context.ServiceScope = serviceScope;
        return context;
    }
}
