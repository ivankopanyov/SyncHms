namespace SyncHms.Cache;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Метод, расширяющий интерфейс <see cref="IServiceCollection"/>.<br/>
    /// Регистрирует сервис кеширования в контейнере зависимостей.
    /// </summary>
    /// <typeparam name="TImplement">
    /// Тип класса сервиса кеширования.<br/>
    /// Должен имплементировать интерфейс <see cref="ICache"/>
    /// </typeparam>
    /// <param name="services">Текущий объект <see cref="IServiceCollection"/>.</param>
    /// <returns>Текущий объект <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddCache<TImplement>(this IServiceCollection services)
        where TImplement : class, ICache
    {
        return services.AddSingleton<ICache, TImplement>();
    }
    
    /// <summary>
    /// Метод, расширяющий интерфейс <see cref="IServiceCollection"/>.<br/>
    /// Регистрирует сервис кеширования в контейнере зависимостей.
    /// </summary>
    /// <typeparam name="TImplement">
    /// Тип класса сервиса кеширования.<br/>
    /// Должен имплементировать интерфейс <see cref="ICache"/>
    /// </typeparam>
    /// <typeparam name="TOptions">
    /// Тип класса опций сервиса кеширования.<br/>
    /// Класс должен иметь публичный конструктор без параметров.
    /// </typeparam>
    /// <param name="services">Текущий объект <see cref="IServiceCollection"/>.</param>
    /// <param name="options">Делегат метода инициализации оциий сервиса кеширования.</param>
    /// <returns>Текущий объект <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddCache<TImplement, TOptions>(this IServiceCollection services, Action<TOptions>? options = null)
        where TImplement : class, ICache where TOptions : class, new()
    {
        var cacheOptions = new TOptions();
        options?.Invoke(cacheOptions);
        services.AddSingleton(cacheOptions);

        return services.AddSingleton<ICache, TImplement>();
    }
}
