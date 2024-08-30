namespace SyncHms.Domain.EntityFramework;

/// <summary>
/// Статический класс, который содержит методы расширения для интерфейса <see cref="ICacheBuilder"/>
/// </summary>
public static class CacheBuilderExtensions
{
    /// <summary>
    /// Метод регистрирует сервисы и репозитории, хранящие данные приложения
    /// в реляционной базе данных, в контейнере зависимостей.
    /// </summary>
    /// <typeparam name="TContext">Тип контекста базы данных.</typeparam>
    /// <typeparam name="TEnvironment">Тип окружения.</typeparam>
    /// <param name="optionsBuilder">Инициализация опций домена.</param>
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
    
    /// <summary>Метод регистрирует репозитории в контейнере зависимостей.</summary>
    /// <typeparam name="TEnvironment">Тип окружения.</typeparam>
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
