namespace SyncHms.Cache.Implement;

internal class CacheBuilder(IServiceCollection services) : ICacheBuilder
{
    public IServiceCollection Services => services;
}