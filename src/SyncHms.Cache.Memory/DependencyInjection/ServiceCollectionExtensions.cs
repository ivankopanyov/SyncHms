namespace SyncHms.Cache;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCacheMemory(this IServiceCollection services)
    {
        return services.AddSingleton<IMemoryCache, MemoryCache>();
    }
}