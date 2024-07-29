namespace SyncHms.Identity;

public static class CacheBuilderExtensions
{
    public static void AddSqliteIdentity(this ICacheBuilder cacheBuilder,
        Action<SqliteOptions>? optionsBuilder = null)
    {
        var sqliteOptions = new SqliteOptions
        {
            ConnectionString = "Data Source=identity.db"
        };
        
        optionsBuilder?.Invoke(sqliteOptions);
        cacheBuilder.AddIdentity(builder =>
        {
            builder.AccessTokenExpirationMinutes = sqliteOptions.AccessTokenExpirationMinutes;
            builder.RefreshTokenExpirationDays = sqliteOptions.RefreshTokenExpirationDays;
            builder.InitDatabase = sqliteOptions.InitDatabase;
            builder.OnConfiguring(options =>
            {
                options.UseSqlite(sqliteOptions.ConnectionString);
            });
        });
    }
}