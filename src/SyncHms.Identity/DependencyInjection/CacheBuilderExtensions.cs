namespace SyncHms.Identity;

public static class CacheBuilderExtensions
{
    public static ICacheBuilder AddIdentity<TContext>(this ICacheBuilder cacheBuilder,
        Action<IdentityOptions>? setupAction = null) where TContext : IdentityContext
    {
        var options = new IdentityOptions();
        setupAction?.Invoke(options);
        
        cacheBuilder
            .AddScoped<IConnectionRepository, ConnectionRepository>()
            .AddScoped<ITokenService, JwtService>()
            .AddScoped<IIdentityService, IdentityService>()
            .AddSingleton<IPostConfigureOptions<JwtBearerOptions>, Infrastructure.JwtBearerPostConfigureOptions>()
            .AddSingleton<IUserIdProvider, UserIdProvider>()
            .AddSingleton<IIdentityContextFactory, IdentityContextFactory<TContext>>()
            .AddHostedService<InitialService>()
            .AddDbContext<TContext>();
        
        var identityBuilder = options.SetupAction != null 
            ? cacheBuilder.AddIdentity<User, Role>(options.SetupAction)
            : cacheBuilder.AddIdentity<User, Role>();
            
        identityBuilder.AddEntityFrameworkStores<TContext>();
        
        cacheBuilder
            .AddSingleton(options)
            .AddAuthentication()
            .AddJwtBearer()
            .AddJwtBearer("Refresh");

        return cacheBuilder;
    }
}
