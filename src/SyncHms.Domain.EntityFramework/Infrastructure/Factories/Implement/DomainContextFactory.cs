namespace SyncHms.Domain.EntityFramework.Infrastructure.Factories.Implement;

public class DomainContextFactory<TContext> : IDomainContextFactory where TContext : DomainContext
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public DomainContextFactory(IServiceScopeFactory serviceScopeFactory, EntityFrameworkDomainOptions options)
    {
        _serviceScopeFactory = serviceScopeFactory;
        using var context = Create();
        if (options.UseMigrations)
            context.Database.Migrate();
        else
            context.Database.EnsureCreated();
    }
    
    public DomainContext Create()
    {
        var serviceScope = _serviceScopeFactory.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<TContext>();
        context.ServiceScope = serviceScope;
        return context;
    }
}
