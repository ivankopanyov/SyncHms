namespace SyncHms.Bus;

public static class ServiceCollectionExtensions
{
    public static IBusBuilder AddSqliteBus(this IServiceCollection services,
        Action<DatabaseOptions>? optionsBuilder = null)
    {
        var sqliteOptions = new DatabaseOptions
        {
            ConnectionString = "Data Source=bus.db"
        };
        
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
