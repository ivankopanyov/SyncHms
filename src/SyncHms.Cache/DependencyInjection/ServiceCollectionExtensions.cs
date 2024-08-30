namespace SyncHms.Cache;

/// <summary>
/// Класс методов расширения интерфейса <see cref="IServiceCollection"/>.
/// Содержит методы для добавления сервиса кеширования в контейнер зависимостей.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>Метод добавляет сервис кеширования в контейнер зависимостей.</summary>
    /// <typeparam name="TImplement">
    /// Тип имплементации сервиса кеширования.
    /// Должен имплементировать интерфейс <see cref="ICache"/>.
    /// </typeparam>
    public static ICacheBuilder AddCache<TImplement>(this IServiceCollection services)
        where TImplement : class, ICache
    {
        services.AddSingleton<ICache, TImplement>();
        return new CacheBuilder(services);
    }


    /// <summary>Метод добавляет сервис кеширования в контейнер зависимостей.</summary>
    /// <typeparam name="TImplement">
    /// Тип имплементации сервиса кеширования.
    /// Должен имплементировать интерфейс <see cref="ICache"/>.
    /// </typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса кеширования.</typeparam>
    /// <param name="options">Инициализация объекта опций сервиса кеширования.</param>
    public static ICacheBuilder AddCache<TImplement, TOptions>(this IServiceCollection services, Action<TOptions>? options = null)
        where TImplement : class, ICache where TOptions : class, new()
    {
        var cacheOptions = new TOptions();
        options?.Invoke(cacheOptions);
        services
            .AddSingleton(cacheOptions)
            .AddSingleton<ICache, TImplement>();

        return new CacheBuilder(services);
    }
}
