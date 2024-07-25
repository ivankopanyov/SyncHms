namespace SyncHms.Bus;

public static class DependencyInjection
{
    public static BusBuilder AddSqliteBus(this IServiceCollection services, Action<SqliteBusOptions>? optionsBuilder = null)
    {
        var sqliteOptions = new SqliteBusOptions();
        optionsBuilder?.Invoke(sqliteOptions);
        return services.AddEntityFrameworkBus(builder =>
        {
            builder.InitDatabase = sqliteOptions.InitDatabase;
            builder.OnConfiguring(options =>
            {
                options.UseSqlite(sqliteOptions.ConnectionString);
            });
        });
    }
}
