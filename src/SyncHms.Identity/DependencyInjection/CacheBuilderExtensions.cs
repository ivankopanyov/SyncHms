namespace SyncHms.Identity;

/// <summary>
/// Статический класс, который содержит методы расширения для интерфейса <see cref="ICacheBuilder"/>
/// </summary>
public static class CacheBuilderExtensions
{
    /// <summary>Метод регистрирует сервисы идентификации пользователей в контейнере зависимостей.</summary>
    /// <typeparam name="TContext">Тип контекста базы данных.</typeparam>
    /// <param name="setupAction">Инициализация опций сервиса идентификации.</param>
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
            .AddHostedService<InitialWorker>()
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
