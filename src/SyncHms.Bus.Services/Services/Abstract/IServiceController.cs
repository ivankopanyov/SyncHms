namespace SyncHms.Bus.Services;

/// <summary>Метод, обрабатывающий сообщение об изменении состояния подключения сервиса.</summary>
public delegate void ChangedOptionsHandle(UpdatedServiceInfo serviceInfo);

/// <summary>Метод, обрабатывающий отправку обновленных опций сервису.</summary>
internal delegate Task SetOptionsHandleAsync(Options options);

/// <summary>Метод, обрабатывющий отправку уведомления сервису о переподключении к удаленнному ресурсу.</summary>
internal delegate Task ReloadHandleAsync(string serviceName);

/// <summary>Метод, обрабатывающий изменение значений переменных окружения.</summary>
/// <typeparam name="TEnvironment">Тип окружения.</typeparam>
internal delegate void SetEnvironmentHandle<in TEnvironment>(TEnvironment environment) where TEnvironment : class, new();

/// <summary>Интерфейс, определяющий методы контроллера сервисов.</summary>
public interface IServiceController
{
    /// <summary>Событие, вызываемое при получении от сервиса сообщения об измении состояния подключения.</summary>
    event ChangedOptionsHandle? ChangedOptionsEvent;

    /// <summary>Событие, вызываемое при изменении опций сервиса.</summary>
    internal event SetOptionsHandleAsync? SetOptionsEvent;

    /// <summary>Событие, вызываемое при переподключении сервиса к удаленному ресурсу.</summary>
    internal event ReloadHandleAsync? ReloadEvent;

    /// <summary>Метод, вызывающий событие <see cref="ChangedOptionsEvent"/></summary>
    /// <param name="serviceInfo">Экземпляр текущего состояния сервиса.</param>
    internal void ChangedOptions(UpdatedServiceInfo serviceInfo);

    /// <summary>Метод, вызывающий событие <see cref="SetOptionsEvent"/></summary>
    /// <param name="options">Экземпляр обновленных опций сервиса.</param>
    void SetOptions(Options options);

    /// <summary>Метод, вызывающий событие <see cref="ReloadEvent"/></summary>
    /// <param name="serviceName">Имя сервиса.</param>
    void Reload(string serviceName);
}

/// <summary>
/// Интерфейс, определяющий методы контроллера сервисов.<br/>
/// Расширяет методы интерфейса <see cref="IServiceController"/>
/// </summary>
/// <typeparam name="TEnvironment">Тип окружения.</typeparam>
public interface IServiceController<TEnvironment> : IServiceController where TEnvironment : class, new()
{
    /// <summary>Событие, вызываемое при изменении значений пременных окружения.</summary>
    internal event SetEnvironmentHandle<TEnvironment>? SetEnvironmentEvent;

    /// <summary>Метод, вызывающий событие <see cref="SetEnvironmentEvent"/></summary>
    /// <param name="environment">Экземпляр обновленных переменных окружения.</param>
    void SetEnvironment(TEnvironment environment);
}
