namespace SyncHms.Bus.EntityFramework;

/// <summary>Статический класс, содержащий метод расширения для интерфейса <see cref="IServiceCollection"/>.</summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Метод регистрирует шину данных, онованную на реляционной базе данных, в контейнере зависимостей.<br/>
    /// Время жизни сервиса <see cref="ServiceLifetime.Singleton"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// Тип объекта, реализующего интерфейс <see cref="IBusProvider{TOptions}"/>
    /// </typeparam>
    /// <param name="setupAction">Инициализация опций шины данных.</param>
    public static IBusBuilder AddEntityFrameworkBus<TContext>(this IServiceCollection services,
        Action<EntityFrameworkBusOptions> setupAction) where TContext : BusContext
    {
        services.AddDbContext<TContext>();
        services.AddSingleton<IBusContextFactory, BusContextFactory<TContext>>();
        services.AddSingleton<IExchangeRepository, ExchangeRepository>();
        return services.AddBus<EntityFrameworkBusProvider, EntityFrameworkBusOptions>(setupAction);
    }
}
