namespace SyncHms.Domain.EntityFramework.Infrastructure.Factories.Implement;

/// <summary>
/// Класс, описывающий фабрику, создающую экземпляры контекста подключения к базе данных домена.
/// Реализует интерфейс <see cref="IDomainContextFactory"/>
/// </summary>
/// <typeparam name="TContext">Тип экземпляра контекста.</typeparam>
public class DomainContextFactory<TContext> : IDomainContextFactory where TContext : DomainContext
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    /// <summary>Инициализация объекта фабрики контекста.</summary>
    /// <param name="serviceScopeFactory">Фабрика, создающая объект типа <see cref="IServiceScope"/></param>
    /// <param name="options">Опции домена.</param>
    public DomainContextFactory(IServiceScopeFactory serviceScopeFactory, EntityFrameworkDomainOptions options)
    {
        _serviceScopeFactory = serviceScopeFactory;
        using var context = Create();
        if (options.UseMigrations)
            context.Database.Migrate();
        else
            context.Database.EnsureCreated();
    }
    
    /// <summary>Метод, создающий экземпляр контекста подключения к базе данных домена.</summary>
    /// <returns>Экземпляр контекста подключения к базе данных домена.</returns>
    public DomainContext Create()
    {
        var serviceScope = _serviceScopeFactory.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<TContext>();
        context.ServiceScope = serviceScope;
        return context;
    }
}
