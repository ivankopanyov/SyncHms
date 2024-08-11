namespace SyncHms.Identity.Sqlite;

public static class CacheBuilderExtensions
{
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
