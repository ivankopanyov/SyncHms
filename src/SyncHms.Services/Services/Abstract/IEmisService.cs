namespace SyncHms.Services;

/// <summary>Метод, обрабатывающий идентификаторы обновленных профилей.</summary>
public delegate void UpdateProfileHandle(decimal profileNumber);

/// <summary>
/// Интерфейс, определяющий методы сервиса интеграции с удаленным сервисом <c>EMIS</c><br/>
/// Расширяет методы интерфейса <see cref="IService{TOptions,TEnvironment}"/>
/// </summary>
public interface IEmisService : IService<EmisOptions, ApplicationEnvironment>
{
    /// <summary>Флаг, указывающий, включен ли сервис.</summary>
    public bool Enabled { get; }

    /// <summary>Статусы, которые должны быть установлены у бронирований профиля.</summary>
    public ISet<string> Statuses { get; }

    /// <summary>Событие, вызываемое при получении сервисом идентификатора обновленного профиля.</summary>
    event UpdateProfileHandle? UpdateProfileEvent;

    /// <summary>
    /// Метод, вызывающий событие <see cref="UpdateProfileEvent"/>,
    /// если установлено значение <c>true</c> у свойства <see cref="EmisOptions.Enabled"/>
    /// </summary>
    /// <param name="profileNumber">Идентификатора обновленного профиля.</param>
    Task PublishAsync(decimal profileNumber);

    /// <summary>
    /// Метод, отменющий вызов события<see cref="UpdateProfileEvent"/></summary>
    /// для указанного профиля.
    /// <param name="profileNumber">Идентификатора профиля.</param>
    Task CancelAsync(decimal profileNumber);
}