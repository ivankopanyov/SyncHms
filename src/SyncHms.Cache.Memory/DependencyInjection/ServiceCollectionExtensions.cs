namespace SyncHms.Cache;

/// <summary>Статический класс, который содержит методы расширения для интерфейса <see cref="IServiceCollection"/>.</summary>
public static class ServiceCollectionExtensions
{
    /// <summary>Метод, регистрирующий сервис кеширования в памяти приложения в контейнере зависимостей.</summary>
    public static IServiceCollection AddCacheMemory(this IServiceCollection services)
    {
        return services.AddSingleton<IMemoryCache, MemoryCache>();
    }
}