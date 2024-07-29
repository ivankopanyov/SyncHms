namespace SyncHms.Identity;

public static class CacheBuilderExtensions
{
    public static ICacheBuilder AddIdentity<TContext>(this ICacheBuilder cacheBuilder,
        Action<IdentityOptions>? setupAction = null) where TContext : IdentityContext
    {
        var options = new IdentityOptions();
        
        cacheBuilder
            .AddScoped<IConnectionRepository, ConnectionRepository>()
            .AddScoped<ITokenService, JwtService>()
            .AddScoped<IIdentityService, IdentityService>()
            .AddSingleton<IPostConfigureOptions<JwtBearerOptions>, Infrastructure.JwtBearerPostConfigureOptions>()
            .AddSingleton<IUserIdProvider, UserIdProvider>()
            .AddSingleton<IIdentityContextFactory, IdentityContextFactory<TContext>>()
            .AddDbContext<TContext>()
            .AddIdentity<User, Role>(identityOptions =>
            {
                options.Options = identityOptions;
                setupAction?.Invoke(options);
            })
            .AddEntityFrameworkStores<TContext>();
        
        cacheBuilder
            .AddSingleton(options)
            .AddAuthentication()
            .AddJwtBearer()
            .AddJwtBearer("Refresh");

        return cacheBuilder;
    }
}