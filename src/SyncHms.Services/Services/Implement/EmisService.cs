namespace SyncHms.Services.Services.Implement;

/// <summary>
/// Класс, описывающий сервис интеграции с удаленным сервисом <c>EMIS</c><br/>
/// Реализует интерфейс <see cref="IEmisService"/>
/// </summary>
/// <param name="control">Экземпляр контроллера, управляющего состоянием сервиса.</param>
internal class EmisService(IControl<EmisOptions, ApplicationEnvironment> control) : IEmisService
{
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
    public void Publish(decimal profileNumber)
    {
        if (control.Options.Enabled)
            new Thread(SendAsync).Start(profileNumber);
    }

    /// <summary>Метод, вызывающий событие <see cref="UpdateProfileEvent"/></summary>
    /// <param name="profileNumber">Идентификатора обновленного профиля.</param>
    private async void SendAsync(object? profileNumber)
    {
        var number = (decimal)profileNumber!;
        await Task.Delay(TimeSpan.FromSeconds(control.Options.DelaySeconds));
        UpdateProfileEvent?.Invoke(number);
    }
}