namespace SyncHms.Identity.Workers;

/// <summary>
/// Класс, описывающий фоновый сервис инициализации базы данных сервиса идентификации пользователей.<br/>
/// Унаследован от класса <see cref="BackgroundService"/>
/// </summary>
internal class InitialWorker : BackgroundService
{
    /// <summary>Инициализация сервиса.</summary>
    /// <param name="identityContextFactory">Экземпляр фабрики, создающей контекст подключения к базе данных.</param>
    /// <param name="options">Экземпляр опций сервиса идентификации.</param>
    public InitialWorker(IIdentityContextFactory identityContextFactory, IdentityOptions options)
    {
        using var context = identityContextFactory.Create();
        if (options.UseMigrations)
            context.Database.Migrate();
        else
            context.Database.EnsureCreated();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;
}
