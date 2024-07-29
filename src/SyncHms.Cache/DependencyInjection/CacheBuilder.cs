namespace SyncHms.Cache.DependencyInjection;

internal class CacheBuilder(IServiceCollection services) : ApplicationBuilder(services), ICacheBuilder
{
}
