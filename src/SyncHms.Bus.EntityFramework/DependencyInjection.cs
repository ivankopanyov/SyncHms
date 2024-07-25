using SyncHms.Bus.EntityFramework.Infrastructure;

namespace SyncHms.Bus;

public static class DependencyInjection
{
    public static BusBuilder AddEntityFrameworkBus(this IServiceCollection services,
        Action<EntityFrameworkBusOptions> optionsBuilder)
    {
        services.AddDbContext<BusContext>();
        services.AddSingleton<IBusContextFactory, BusContextFactory>();
        services.AddSingleton<IExchangeRepository, ExchangeRepository>();
        return services.AddBus<EntityFrameworkBusProvider, EntityFrameworkBusOptions>(optionsBuilder);
    }
}
