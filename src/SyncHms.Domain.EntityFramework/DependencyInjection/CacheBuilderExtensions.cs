namespace SyncHms.Domain;

public static class CacheBuilderExtensions
{
    public static ICacheBuilder AddEntityFrameworkDomain<TEnvironment>(this ICacheBuilder cacheBuilder,
        Action<EntityFrameworkDomainOptions> optionsBuilder)
        where TEnvironment : class, new()
    {
        var options = new EntityFrameworkDomainOptions();
        optionsBuilder.Invoke(options);
        
        cacheBuilder
            .AddRepositories<TEnvironment>()
            .AddHostedService<DatabaseInitializator>()
            .AddSingleton(options)
            .AddDbContext<DomainContext>();

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
