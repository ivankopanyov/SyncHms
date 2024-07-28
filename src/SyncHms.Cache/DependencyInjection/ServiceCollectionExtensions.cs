namespace SyncHms.Cache;

public static class ServiceCollectionExtensions
{
    public static ICacheBuilder AddCache<TImplement>(this IServiceCollection services)
        where TImplement : class, ICache
    {
        services.AddSingleton<ICache, TImplement>();
        return new CacheBuilder(services);
    }
    
    public static ICacheBuilder AddCache<TImplement, TOptions>(this IServiceCollection services, Action<TOptions>? options = null)
        where TImplement : class, ICache where TOptions : class, new()
    {
        var cacheOptions = new TOptions();
        options?.Invoke(cacheOptions);
        services.AddSingleton(cacheOptions);

        services.AddSingleton<ICache, TImplement>();
        return new CacheBuilder(services);
    }
}
