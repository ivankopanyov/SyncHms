namespace SyncHms.Bus;

public static class DependencyInjection
{
    public static IServiceCollection AddBus<TProvider>(this IServiceCollection services)
        where TProvider : class, IBusProvider
    {
        services.AddSingleton<IBusProvider, TProvider>();
        return services;
    }

    public static IServiceCollection AddBus<TProvider, TOptions>(this IServiceCollection services, Action<TOptions> optionsBuilder)
        where TProvider : class, IBusProvider<TOptions> where TOptions : class, new()
    {
        ArgumentNullException.ThrowIfNull(optionsBuilder, nameof(optionsBuilder));
        var options = new TOptions();
        optionsBuilder.Invoke(options);
        services.AddSingleton(options);
        return services.AddBus<TProvider>();
    }
}
