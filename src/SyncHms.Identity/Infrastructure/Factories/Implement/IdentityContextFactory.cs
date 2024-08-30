namespace SyncHms.Identity.Infrastructure.Factories.Implement;

/// <summary>
/// Класс, описывающий фабрику, создающую контекст подключения к базе данных.<br/>
/// Реализует интерфейс <see cref="IIdentityContextFactory"/>
/// </summary>
/// <typeparam name="TContext">
/// Тип контекста подключения к базе данных сервиса идентификации.<br/>
/// Должен быть унаследован от класса <see cref="IdentityContext"/>
/// </typeparam>
/// <param name="serviceScopeFactory">
/// Экземпляр фабрики, создающей объекты, реализующие интерфейс <see cref="IServiceScope"/>
/// </param>
internal class IdentityContextFactory<TContext>(IServiceScopeFactory serviceScopeFactory)
    : IIdentityContextFactory where TContext : IdentityContext
{
    /// <summary>Метод, создающий контекст подключения к базе данных.</summary>
    /// <returns>Экземпляр контекста подключения к базе данных.</returns>
    public IdentityContext Create()
    {
        var serviceScope = serviceScopeFactory.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<TContext>();
        context.ServiceScope = serviceScope;
        return context;
    }
}
