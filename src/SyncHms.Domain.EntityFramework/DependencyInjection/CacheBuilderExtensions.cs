namespace SyncHms.Domain;

public static class CacheBuilderExtensions
{
    public static IServiceCollection AddEntityFrameworkDomain<TEnvironment>(this ICacheBuilder cacheBuilder,
        Action<EntityFrameworkDomainOptions> optionsBuilder)
        where TEnvironment : class, new()
    {
        var options = new EntityFrameworkDomainOptions();
        optionsBuilder.Invoke(options);
        
        return cacheBuilder.Services
            .AddHostedService<DatabaseInitializator>()
            .AddSingleton(options)
            .AddDbContext<DomainContext>()
            .AddRepositories<TEnvironment>();
    }
    
    private static IServiceCollection AddRepositories<TEnvironment>(this IServiceCollection services)
        where TEnvironment : class, new()
    {
        return services
            .AddScoped<ILogRepository, LogRepository>()
            .AddScoped<IServiceRepository, ServiceRepository>()
            .AddScoped<IEnvironmentRepository<TEnvironment>, EnvironmentRepository<TEnvironment>>();
    }
}
