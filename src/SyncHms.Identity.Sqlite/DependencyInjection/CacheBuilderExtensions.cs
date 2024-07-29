namespace SyncHms.Identity.Sqlite;

public static class CacheBuilderExtensions
{
    public static ICacheBuilder AddSqliteIdentity(this ICacheBuilder cacheBuilder,
        Action<SqliteIdentityOptions>? setupAction = null)
    {
        var sqliteOptions = new SqliteIdentityOptions();
        
        var result = cacheBuilder.AddIdentity<SqliteIdentityContext>(builder =>
        {
            sqliteOptions.Options = builder.Options;
            setupAction?.Invoke(sqliteOptions);
            builder.AccessTokenExpirationMinutes = sqliteOptions.AccessTokenExpirationMinutes;
            builder.RefreshTokenExpirationDays = sqliteOptions.RefreshTokenExpirationDays;
            builder.UseMigrations = sqliteOptions.UseMigrations;
        });
        
        cacheBuilder.AddSingleton(sqliteOptions);
        return result;
    }
}
