namespace SyncHms.Services;

/// <summary>Метод, обрабатывающий идентификаторы обновленных профилей.</summary>
public delegate void UpdateProfileHandle(decimal profileNumber);

/// <summary>
/// Интерфейс, определяющий методы сервиса интеграции с удаленным сервисом <c>EMIS</c><br/>
/// Расширяет методы интерфейса <see cref="IService{TOptions,TEnvironment}"/>
/// </summary>
public interface IEmisService : IService<EmisOptions, ApplicationEnvironment>
{
    /// <summary>Событие, вызываемое при получении сервисом идентификатора обновленного профиля.</summary>
    event UpdateProfileHandle? UpdateProfileEvent;

    /// <summary>
    /// Метод, вызывающий событие <see cref="UpdateProfileEvent"/>,
    /// если установлено значение <c>true</c> у свойства <see cref="EmisOptions.Enabled"/>
    /// </summary>
    /// <param name="profileNumber">Идентификатора обновленного профиля.</param>
    void Publish(decimal profileNumber);
}