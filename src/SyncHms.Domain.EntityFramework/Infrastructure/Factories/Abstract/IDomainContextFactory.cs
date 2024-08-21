namespace SyncHms.Domain.EntityFramework.Infrastructure.Factories.Abstract;

/// <summary>
/// Интерфейс, содержащий методы фабрики, создающей
/// экземпляры контекста подключения к базе данных домена.
/// </summary>
internal interface IDomainContextFactory
{
    /// <summary>Метод, создающий экземпляр контекста подключения к базе данных домена.</summary>
    /// <returns>Экземпляр контекста подключения к базе данных домена.</returns>
    DomainContext Create();
}