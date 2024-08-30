namespace SyncHms.Bus;

/// <summary>
/// Статический класс, который содержит методы расширения для интерфейса <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Метод регистрирует шину данных в контейнере зависимостей.
    /// Время жизни сервиса <see cref="ServiceLifetime.Singleton"/>.
    /// </summary>
    /// <typeparam name="TProvider">
    /// Тип объекта, реализующего интерфейс <see cref="IBusProvider{TOptions}"/>
    /// </typeparam>
    /// <typeparam name="TOptions">
    /// Тип объекта, содержащего опции установки шины данных.
    /// Должен иметь конструктор без параметров.
    /// </typeparam>
    /// <param name="setupAction">Инициализация опций шины данных.</param>
    /// <exception cref="ArgumentNullException"/>
    public static IBusBuilder AddBus<TProvider, TOptions>(this IServiceCollection services, Action<TOptions> setupAction)
        where TProvider : class, IBusProvider<TOptions> where TOptions : class, new()
    {
        ArgumentNullException.ThrowIfNull(setupAction, nameof(setupAction));
        var options = new TOptions();
        setupAction.Invoke(options);
        services.AddSingleton(options);
        services.AddSingleton<IBusProvider, TProvider>();
        return new BusBuilder(services);
    }
}
