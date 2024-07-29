namespace SyncHms.Domain.EntityFramework.Sqlite;

public static class CacheBuilderExtensions
{
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
