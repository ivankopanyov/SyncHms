namespace SyncHms.Identity;

public static class CacheBuilderExtensions
{
    public static void AddIdentity(this ICacheBuilder builder,
        Action<IdentityOptions> optionsBuilder)
    {
        var options = new IdentityOptions();
        optionsBuilder?.Invoke(options);
        
        builder.Services
            .AddSingleton(options)
            .AddScoped<IConnectionRepository, ConnectionRepository>()
            .AddScoped<ITokenService, JwtService>()
            .AddScoped<IIdentityService, IdentityService>()
            .AddSingleton<IPostConfigureOptions<JwtBearerOptions>, Infrastructure.JwtBearerPostConfigureOptions>()
            .AddSingleton<IUserIdProvider, UserIdProvider>()
            .AddHostedService<DatabaseInitializator>()
            .AddDbContext<IdentityContext>()
            .AddIdentity<User, Role>(identityOptions =>
            {
                identityOptions.Password.RequiredUniqueChars = 0;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireLowercase = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<IdentityContext>();
        
        builder.Services
            .AddAuthentication()
            .AddJwtBearer()
            .AddJwtBearer("Refresh");
    }
}