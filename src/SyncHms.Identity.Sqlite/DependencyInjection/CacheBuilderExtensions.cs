namespace SyncHms.Identity;

public static class CacheBuilderExtensions
{
    public static ICacheBuilder AddSqliteIdentity(this ICacheBuilder cacheBuilder,
        Action<SqliteIdentityOptions>? optionsBuilder = null)
    {
        var sqliteOptions = new SqliteIdentityOptions();
        optionsBuilder?.Invoke(sqliteOptions);
        cacheBuilder.AddSingleton(sqliteOptions);
        
        return cacheBuilder.AddIdentity<SqliteIdentityContext>(builder =>
        {
            builder.AccessTokenExpirationMinutes = sqliteOptions.AccessTokenExpirationMinutes;
            builder.RefreshTokenExpirationDays = sqliteOptions.RefreshTokenExpirationDays;
            builder.UseMigrations = sqliteOptions.UseMigrations;
        });
    }
}