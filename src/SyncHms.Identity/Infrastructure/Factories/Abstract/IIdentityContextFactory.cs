namespace SyncHms.Identity.Infrastructure.Factories.Abstract;

/// <summary>Интерфейс, определяющий методы фабрики, создающей контекст подключения к базе данных.</summary>
internal interface IIdentityContextFactory
{
    /// <summary>Метод, создающий контекст подключения к базе данных.</summary>
    /// <returns>Экземпляр контекста подключения к базе данных.</returns>
    IdentityContext Create();
}