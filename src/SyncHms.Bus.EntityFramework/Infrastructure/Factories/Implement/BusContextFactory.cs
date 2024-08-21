namespace SyncHms.Bus.EntityFramework.Infrastructure.Factories.Implement;

/// <summary>
/// Класс, описывающий фабрику, создающую экземпляры контекста подключения к базе данных шины.
/// Реализует интерфейс <see cref="IBusContextFactory"/>
/// </summary>
/// <typeparam name="TContext">Тип экземпляра контекста.</typeparam>
internal class BusContextFactory<TContext> : IBusContextFactory where TContext : BusContext
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    /// <summary>Инициализация объекта фабрики контекста.</summary>
    /// <param name="serviceScopeFactory">Фабрика, создающая объект типа <see cref="IServiceScope"/></param>
    /// <param name="options">Опции шины данных.</param>
    public BusContextFactory(IServiceScopeFactory serviceScopeFactory, EntityFrameworkBusOptions options)
    {
        _serviceScopeFactory = serviceScopeFactory;
        using var context = Create();
        if (options.UseMigrations)
            context.Database.Migrate();
        else
            context.Database.EnsureCreated();
    }

    /// <summary>Метод, создающий экземпляр контекста подключения к базе данных шины.</summary>
    /// <returns>Экземпляр контекста подключения к базе данных шины.</returns>
    public BusContext Create()
    {
        var serviceScope = _serviceScopeFactory.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<TContext>();
        context.ServiceScope = serviceScope;
        return context;
    } 
}
