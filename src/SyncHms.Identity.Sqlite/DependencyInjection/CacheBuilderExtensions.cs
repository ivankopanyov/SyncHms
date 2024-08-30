namespace SyncHms.Identity.Sqlite;

/// <summary>
/// Статический класс, который содержит методы расширения для интерфейса <see cref="ICacheBuilder"/>
/// </summary>
public static class CacheBuilderExtensions
{
    /// <summary>
    /// Метод регистрирует сервисы идентификации пользователей в контейнере зависимостей,
    /// хранящие данные пользователей в базе данных <c>SQLite</c>
    /// </summary>
    /// <param name="setupAction">Инициализация опций сервисов идентификации.</param>
    /// <returns></returns>
    public static ICacheBuilder AddSqliteIdentity(this ICacheBuilder cacheBuilder,
        Action<SqliteIdentityOptions>? setupAction = null)
    {
        var sqliteOptions = new SqliteIdentityOptions();
        setupAction?.Invoke(sqliteOptions);
        
        var result = cacheBuilder
            .AddIdentity<SqliteIdentityContext>(options =>
            {
                options.AccessTokenExpirationMinutes = sqliteOptions.AccessTokenExpirationMinutes;
                options.RefreshTokenExpirationDays = sqliteOptions.RefreshTokenExpirationDays;
                options.UseMigrations = sqliteOptions.UseMigrations;
                options.SetupAction = sqliteOptions.SetupAction;
            });
        
        cacheBuilder.AddSingleton(sqliteOptions);
        return result;
    }
}
