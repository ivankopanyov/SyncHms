namespace SyncHms.Events;

/// <summary>
/// Статический класс, который содержит методы расширения для интерфейса <see cref="IApplicationServicesBuilder"/>
/// </summary>
public static class ApplicationServicesBuilderExtensions
{
    /// <summary>Метод, регистрирующий сервис кеширования в контейнере зависимостей.</summary>
    /// <typeparam name="TImplement">
    /// Тип сервиса кештрования.<br/>
    /// Должен реализовывать интерфейс <see cref="ICache"/>
    /// </typeparam>
    /// <returns>Экземпляр построителя, регистрирующего обработчики событий.</returns>
    public static IApplicationEventsBuilder AddCache<TImplement>(
        this IApplicationServicesBuilder applicationServicesBuilder)
        where TImplement : class, ICache
    {
        ((IServiceCollection)applicationServicesBuilder).AddCache<TImplement>();
        return new ApplicationEventsBuilder(applicationServicesBuilder);
    }
    
    /// <summary>Метод, регистрирующий сервис кеширования в контейнере зависимостей.</summary>
    /// <typeparam name="TImplement">
    /// Тип сервиса кештрования.<br/>
    /// Должен реализовывать интерфейс <see cref="ICache"/>
    /// </typeparam>
    /// <typeparam name="TOptions">
    /// Тип опций сервиса кеширования. Должен иметь открытый конструктор без параметров.
    /// </typeparam>
    /// <param name="options">Инициализация опций сервиса кеширования.</param>
    /// <returns>Экземпляр построителя, регистрирующего обработчики событий.</returns>
    public static IApplicationEventsBuilder AddCache<TImplement, TOptions>(
        this IApplicationServicesBuilder applicationServicesBuilder, Action<TOptions>? options = null)
        where TImplement : class, ICache where TOptions : class, new()
    {
        ((IServiceCollection)applicationServicesBuilder).AddCache<TImplement, TOptions>(options);
        return new ApplicationEventsBuilder(applicationServicesBuilder);
    }
}
