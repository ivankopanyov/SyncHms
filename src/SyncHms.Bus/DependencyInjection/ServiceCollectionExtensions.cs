namespace SyncHms.Bus;

public static class ServiceCollectionExtensions
{
    public static IBusBuilder AddBus<TProvider, TOptions>(this IServiceCollection services, Action<TOptions> optionsBuilder)
        where TProvider : class, IBusProvider<TOptions> where TOptions : class, new()
    {
        ArgumentNullException.ThrowIfNull(optionsBuilder, nameof(optionsBuilder));
        var options = new TOptions();
        optionsBuilder.Invoke(options);
        services.AddSingleton(options);
        services.AddSingleton<IBusProvider, TProvider>();
        return new BusBuilder(services);
    }
}
