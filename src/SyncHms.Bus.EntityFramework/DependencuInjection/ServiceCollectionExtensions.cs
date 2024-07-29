namespace SyncHms.Bus;

public static class ServiceCollectionExtensions
{
    public static IBusBuilder AddEntityFrameworkBus<TContext>(this IServiceCollection services,
        Action<EntityFrameworkBusOptions> optionsBuilder) where TContext : BusContext
    {
        services.AddDbContext<TContext>();
        services.AddSingleton<IBusContextFactory, BusContextFactory<TContext>>();
        services.AddSingleton<IExchangeRepository, ExchangeRepository>();
        return services.AddBus<EntityFrameworkBusProvider, EntityFrameworkBusOptions>(optionsBuilder);
    }
}
