namespace SyncHms.Domain.EntityFramework;

public static class CacheBuilderExtensions
{
    public static ICacheBuilder AddEntityFrameworkDomain<TContext, TEnvironment>(this ICacheBuilder cacheBuilder,
        Action<EntityFrameworkDomainOptions>? optionsBuilder = null)
        where TContext : DomainContext where TEnvironment : class, new()
    {
        var options = new EntityFrameworkDomainOptions();
        optionsBuilder?.Invoke(options);
        
        cacheBuilder
            .AddRepositories<TEnvironment>()
            .AddSingleton<IDomainContextFactory, DomainContextFactory<TContext>>()
            .AddSingleton(options)
            .AddDbContext<TContext>();

        return cacheBuilder;
    }
    
    private static ICacheBuilder AddRepositories<TEnvironment>(this ICacheBuilder cacheBuilder)
        where TEnvironment : class, new()
    {
        cacheBuilder
            .AddScoped<ILogRepository, LogRepository>()
            .AddScoped<IServiceRepository, ServiceRepository>()
            .AddScoped<IEnvironmentRepository<TEnvironment>, EnvironmentRepository<TEnvironment>>();

        return cacheBuilder;
    }
}
