namespace SyncHms.Bus.EntityFramework.Sqlite;


/// <summary>Статический класс, содержащий метод расширения для интерфейса <see cref="IServiceCollection"/>.</summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Метод регистрирует шину данных, онованную на <c>SQLite</c>, в контейнере зависимостей.<br/>
    /// Время жизни сервиса <see cref="ServiceLifetime.Singleton"/>.
    /// </summary>
    /// <param name="setupAction">Инициализация опций шины данных.</param>
    public static IBusBuilder AddSqliteBus(this IServiceCollection services,
        Action<SqliteBusOptions>? setupAction = null)
    {
        var sqliteOptions = new SqliteBusOptions();
        setupAction?.Invoke(sqliteOptions);
        return services
            .AddSingleton(sqliteOptions)
            .AddEntityFrameworkBus<SqliteBusContext>(builder => 
            {
                builder.UseMigrations = sqliteOptions.UseMigrations;
                builder.ContractResolver = sqliteOptions.ContractResolver;
            });
    }
}
