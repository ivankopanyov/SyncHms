namespace SyncHms.Bus;

public static class ServiceCollectionExtensions
{
    public static IBusBuilder AddBus<TProvider, TOptions>(this IServiceCollection services, Action<TOptions> setupAction)
        where TProvider : class, IBusProvider<TOptions> where TOptions : class, new()
    {
        ArgumentNullException.ThrowIfNull(setupAction, nameof(setupAction));
        var options = new TOptions();
        setupAction.Invoke(options);
        services.AddSingleton(options);
        services.AddSingleton<IBusProvider, TProvider>();
        return new BusBuilder(services);
    }
}
