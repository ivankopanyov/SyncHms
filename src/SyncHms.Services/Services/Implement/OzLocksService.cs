namespace SyncHms.Services.Services.Implement;

/// <summary>
/// Класс, описывающий сервис интеграции с базой данных <c>OzLocks</c><br/>
/// Реализует интерфейс <see cref="IOzLocksService"/>
/// </summary>
/// <param name="control">Экземпляр контроллера, управляющего состоянием сервиса.</param>
internal class OzLocksService(IControl<OzLocksOptions, ApplicationEnvironment> control) : IOzLocksService
{
    /// <summary>Свойство, возвращающее новый экземпляр контекста для подключения к базе данных.</summary>
    private OzLocksContext Context => new(control.Options.ConnectionString);

    /// <summary>Экземпляр окружения.</summary>
    public ApplicationEnvironment Environment => control.Environment;

    /// <summary>Метод, обрабатывающий изменение опций сервиса.</summary>
    /// <param name="options">Экземпляр опций сервиса.</param>
    public async Task ChangedOptionsHandleAsync(OzLocksOptions options)
    {
        await using var context = Context;
        await context.Guests.AsNoTracking().AnyAsync();
    }

    /// <summary>Метод, обрабатывающий изменение значений переменных окружения.</summary>
    /// <param name="current">Текущее окружение.</param>
    /// <param name="previous">Измененное окружение.</param>
    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current, ApplicationEnvironment previous) => Task.CompletedTask;
}
