namespace SyncHms.Bus.EntityFramework.Infrastructure.Factories.Abstract;

/// <summary>
/// Интерфейс, содержащий методы фабрики, создающей
/// экземпляры контекста подключения к базе данных шины.
/// </summary>
internal interface IBusContextFactory
{
    /// <summary>Метод, создающий экземпляр контекста подключения к базе данных шины.</summary>
    /// <returns>Экземпляр контекста подключения к базе данных шины.</returns>
    BusContext Create();
}
