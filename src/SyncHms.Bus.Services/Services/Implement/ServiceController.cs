namespace SyncHms.Bus.Services.Services.Implement;

/// <summary>
/// Класс, описывающий контроллер сервисов.<br/>
/// Реализует интерфейс <see cref="IServiceController"/>
/// </summary>
internal class ServiceController : IServiceController
{
    /// <summary>Событие, вызываемое при получении от сервиса сообщения об измении состояния подключения.</summary>
    public event ChangedOptionsHandle? ChangedOptionsEvent;

    /// <summary>Событие, вызываемое при изменении опций сервиса.</summary>
    public event SetOptionsHandleAsync? SetOptionsEvent;

    /// <summary>Событие, вызываемое при переподключении сервиса к удаленному ресурсу.</summary>
    public event ReloadHandleAsync? ReloadEvent;

    /// <summary>Метод, вызывающий событие <see cref="ChangedOptionsEvent"/></summary>
    /// <param name="serviceInfo">Экземпляр текущего состояния сервиса.</param>
    public void ChangedOptions(UpdatedServiceInfo serviceInfo) => ChangedOptionsEvent?.Invoke(serviceInfo);

    /// <summary>Метод, вызывающий событие <see cref="SetOptionsEvent"/></summary>
    /// <param name="options">Экземпляр обновленных опций сервиса.</param>
    public void SetOptions(Options options) => SetOptionsEvent?.Invoke(options);

    /// <summary>Метод, вызывающий событие <see cref="ReloadEvent"/></summary>
    /// <param name="serviceName">Имя сервиса.</param>
    public void Reload(string serviceName) => ReloadEvent?.Invoke(serviceName);
}

/// <summary>
/// Класс, описывающий контроллер сервисов.<br/>
/// Унаследован от класса <see cref="ServiceController"/><br/>
/// Реализует интерфейс <see cref="IServiceController{TEnvironment}"/>
/// </summary>
internal class ServiceController<TEnvironment> : ServiceController, IServiceController<TEnvironment>
    where TEnvironment : class, new()
{
    /// <summary>Событие, вызываемое при изменении значений пременных окружения.</summary>
    public event SetEnvironmentHandle<TEnvironment>? SetEnvironmentEvent;

    /// <summary>Метод, вызывающий событие <see cref="SetEnvironmentEvent"/></summary>
    /// <param name="environment">Экземпляр обновленных переменных окружения.</param>
    public void SetEnvironment(TEnvironment environment) => SetEnvironmentEvent?.Invoke(environment);
}
