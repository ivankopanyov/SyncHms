namespace SyncHms.Domain;

public static class CacheBuilderExtensions
{
    public static ICacheBuilder AddSqliteDomain<TEnvironment>(this ICacheBuilder cacheBuilder,
        Action<DatabaseOptions>? optionsBuilder = null) where TEnvironment : class, new ()
    {
        var sqliteOptions = new DatabaseOptions
        {
            ConnectionString = "Data Source=app.db"
        };
        
        optionsBuilder?.Invoke(sqliteOptions);
        return cacheBuilder.AddEntityFrameworkDomain<TEnvironment>(builder =>
        {
            builder.InitDatabase = sqliteOptions.InitDatabase;
            builder.OnConfiguring(options =>
            {
                options.UseSqlite(sqliteOptions.ConnectionString);
            });
        });
    }
}
