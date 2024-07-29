namespace SyncHms.Events;

public static class ApplicationServicesBuilderExtensions
{
    public static IApplicationEventsBuilder AddCache<TImplement>(this IApplicationServicesBuilder applicationServicesBuilder)
        where TImplement : class, ICache
    {
        ((IServiceCollection)applicationServicesBuilder).AddCache<TImplement>();
        return new ApplicationEventsBuilder(applicationServicesBuilder);
    }
    
    public static IApplicationEventsBuilder AddCache<TImplement, TOptions>(this IApplicationServicesBuilder applicationServicesBuilder,
        Action<TOptions>? options = null)
        where TImplement : class, ICache where TOptions : class, new()
    {
        ((IServiceCollection)applicationServicesBuilder).AddCache<TImplement, TOptions>(options);
        return new ApplicationEventsBuilder(applicationServicesBuilder);
    }
}
