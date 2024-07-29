namespace SyncHms.Bus.EntityFramework.Sqlite;

public static class ServiceCollectionExtensions
{
    public static IBusBuilder AddSqliteBus(this IServiceCollection services,
        Action<SqliteBusOptions>? setupAction = null)
    {
        var sqliteOptions = new SqliteBusOptions();
        setupAction?.Invoke(sqliteOptions);
        return services
            .AddSingleton(sqliteOptions)
            .AddEntityFrameworkBus<SqliteBusContext>(builder => 
            {
                builder.UseMigrations = sqliteOptions.UseMigrations;
                builder.ContractResolver = sqliteOptions.ContractResolver;
            });
    }
}
