namespace SyncHms.Identity;

public static class CacheBuilderExtensions
{
    public static ICacheBuilder AddIdentity<TContext>(this ICacheBuilder cacheBuilder,
        Action<IdentityOptions>? optionsBuilder = null) where TContext : IdentityContext
    {
        var options = new IdentityOptions();
        optionsBuilder?.Invoke(options);
        
        cacheBuilder
            .AddSingleton(options)
            .AddScoped<IConnectionRepository, ConnectionRepository>()
            .AddScoped<ITokenService, JwtService>()
            .AddScoped<IIdentityService, IdentityService>()
            .AddSingleton<IPostConfigureOptions<JwtBearerOptions>, Infrastructure.JwtBearerPostConfigureOptions>()
            .AddSingleton<IUserIdProvider, UserIdProvider>()
            .AddSingleton<IIdentityContextFactory, IdentityContextFactory<TContext>>()
            .AddDbContext<TContext>()
            .AddIdentity<User, Role>(identityOptions =>
            {
                identityOptions.Password.RequiredUniqueChars = 0;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireLowercase = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<TContext>();
        
        cacheBuilder
            .AddAuthentication()
            .AddJwtBearer()
            .AddJwtBearer("Refresh");

        return cacheBuilder;
    }
}