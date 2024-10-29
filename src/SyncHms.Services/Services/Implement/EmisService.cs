namespace SyncHms.Services.Services.Implement;

/// <summary>
/// Класс, описывающий сервис интеграции с удаленным сервисом <c>EMIS</c><br/>
/// Реализует интерфейс <see cref="IEmisService"/>
/// </summary>
/// <param name="control">Экземпляр контроллера, управляющего состоянием сервиса.</param>
/// <param name="cache">Экземпляр сервиса кеширования.</param>
internal class EmisService(IControl<EmisOptions, ApplicationEnvironment> control, IMemoryCache cache) : IEmisService
{
    /// <summary>Флаг, указывающий, включен ли сервис.</summary>
    public bool Enabled => control.Options.Enabled;

    /// <summary>Статусы, которые должны быть установлены у бронирований профиля.</summary>
    public ISet<string> Statuses => control.Options.Statuses?
        .Where(s => s.Value)
        .Select(s => s.Key)
        .ToHashSet() ?? [];

    /// <summary>Экземпляр окружения.</summary>
    public ApplicationEnvironment Environment => control.Environment;

    /// <summary>Событие, вызываемое при получении сервисом идентификатора обновленного профиля.</summary>
    public event UpdateProfileHandle? UpdateProfileEvent;

    /// <summary>Метод, обрабатывающий изменение опций сервиса.</summary>
    /// <param name="options">Экземпляр опций сервиса.</param>
    /// <exception cref="Exception">
    /// Исключение возбуждается, если установлено значение <c>false</c> у свойства
    /// <see cref="EmisOptions.Enabled"/> в значении свойства
    /// <see cref="IControl{FiasServiceOptions, ApplicationEnvironment}.Options"/> параметра <c>control</c>
    /// </exception>
    public Task ChangedOptionsHandleAsync(EmisOptions options)
    {
        if (!options.Enabled)
            throw new Exception("Service is disabled");

        return Task.CompletedTask;
    }

    /// <summary>Метод, обрабатывающий изменение значений переменных окружения.</summary>
    /// <param name="current">Текущее окружение.</param>
    /// <param name="previous">Измененное окружение.</param>
    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current, ApplicationEnvironment previous)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Метод, вызывающий событие <see cref="UpdateProfileEvent"/>,
    /// если установлено значение <c>true</c> у свойства
    /// <see cref="EmisOptions.Enabled"/> в значении свойства
    /// <see cref="IControl{FiasServiceOptions, ApplicationEnvironment}.Options"/> параметра <c>control</c>
    /// </summary>
    /// <param name="profileNumber">Идентификатора обновленного профиля.</param>
    public async Task PublishAsync(decimal profileNumber)
    {
        if (!control.Options.Enabled || Statuses.Count == 0)
            return;

        if (control.Options.DelaySeconds == 0)
            UpdateProfileEvent?.Invoke(profileNumber);
        else
            await cache.PushAsync(
                profileNumber.ToString("0"),
                new Profile(),
                TimeSpan.FromSeconds(control.Options.DelaySeconds),
                (_, _) =>
                {
                    UpdateProfileEvent?.Invoke(profileNumber);
                    return Task.CompletedTask;
                });
    }

    /// <summary>
    /// Метод, отменющий вызов события<see cref="UpdateProfileEvent"/></summary>
    /// для указанного профиля.
    /// <param name="profileNumber">Идентификатора профиля.</param>
    public async Task CancelAsync(decimal profileNumber)
    {
        await cache.PopAsync<Profile>(profileNumber.ToString("0"));
    }
}
