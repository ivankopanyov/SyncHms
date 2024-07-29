namespace SyncHms.Bus;

public static class ServiceCollectionExtensions
{
    public static IBusBuilder AddSqliteBus(this IServiceCollection services,
        Action<SqliteBusOptions>? optionsBuilder = null)
    {
        var sqliteOptions = new SqliteBusOptions();
        optionsBuilder?.Invoke(sqliteOptions);
        return services
            .AddSingleton(sqliteOptions)
            .AddEntityFrameworkBus<SqliteBusContext>(builder => 
            {
                builder.UseMigrations = sqliteOptions.UseMigrations;
                builder.ContractResolver = sqliteOptions.ContractResolver;
            });
    }
}
