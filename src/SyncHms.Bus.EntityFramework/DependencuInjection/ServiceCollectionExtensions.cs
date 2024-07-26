namespace SyncHms.Bus;

public static class ServiceCollectionExtensions
{
    public static IBusBuilder AddEntityFrameworkBus(this IServiceCollection services,
        Action<EntityFrameworkBusOptions> optionsBuilder)
    {
        services.AddDbContext<BusContext>();
        services.AddSingleton<IBusContextFactory, BusContextFactory>();
        services.AddSingleton<IExchangeRepository, ExchangeRepository>();
        return services.AddBus<EntityFrameworkBusProvider, EntityFrameworkBusOptions>(optionsBuilder);
    }
}
