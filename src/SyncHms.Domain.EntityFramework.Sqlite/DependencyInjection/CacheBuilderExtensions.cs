namespace SyncHms.Domain.EntityFramework.Sqlite;

/// <summary>
/// Статический класс, который содержит методы расширения для интерфейса <see cref="ICacheBuilder"/>
/// </summary>
public static class CacheBuilderExtensions
{
    /// <summary>
    /// Метод регистрирует сервисы и репозитории, хранящие данные приложения в базе данных <c>SQLite</c>.
    /// </summary>
    /// <typeparam name="TEnvironment">Тип окружения.</typeparam>
    /// <param name="optionsBuilder">Инициализация опций домена.</param>
    public static ICacheBuilder AddSqliteDomain<TEnvironment>(this ICacheBuilder cacheBuilder,
        Action<SqliteDomainOptions>? optionsBuilder = null) where TEnvironment : class, new ()
    {
        var sqliteOptions = new SqliteDomainOptions();
        optionsBuilder?.Invoke(sqliteOptions);
        cacheBuilder.AddSingleton(sqliteOptions);
        return cacheBuilder.AddEntityFrameworkDomain<SqliteDomainContext, TEnvironment>(builder =>
        {
            builder.ContractResolver = sqliteOptions.ContractResolver;
            builder.UseMigrations = sqliteOptions.UseMigrations;
        });
    }
}
