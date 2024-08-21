namespace SyncHms.Bus.Services;

/// <summary>
/// Метод, обрабатывающий отправку сообщения об удачном
/// подключении сервиса к удаленному ресурсу.
/// </summary>
internal delegate Task ActiveHandleAsync();

/// <summary>
/// Метод, обрабатывающий отправку сообщения о неудачной
/// попытке подключении сервиса к удаленному ресурсу.
/// </summary>
internal delegate Task UnactiveHandleAsync(string error, Exception? ex = null);

/// <summary>Интерфейс, определяющий методы контроллера сервиса.</summary>
/// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
public interface IControl<TOptions> where TOptions : class, new()
{
    /// <summary>
    /// Событие, вызываемое при отправке сообщения об удачном
    /// подключении сервиса к удаленному ресурсу.
    /// </summary>
    internal event ActiveHandleAsync? ActiveEvent;

    /// <summary>
    /// Событие, вызываемое при отправке сообщения о неудачной
    /// попытке подключении сервиса к удаленному ресурсу.
    /// </summary>
    internal event UnactiveHandleAsync? UnactiveEvent;

    /// <summary>Экземпляр текущих опций сервиса.</summary>
    TOptions Options { get; internal set; }

    /// <summary>Метод, вызывающий событие <see cref="ActiveEvent"/></summary>
    void Active();

    /// <summary>Метод, вызывающий событие <see cref="UnactiveEvent"/></summary>
    /// <param name="error">
    /// Сообщение об ошибке, вызванной во время подключения сервиса к удаленному ресурсу.
    /// </param>
    void Unactive(string error);

    /// <summary>Метод, вызывающий событие <see cref="UnactiveEvent"/></summary>
    /// <param name="ex">
    /// Исключение, возбужденное во время подключения сервиса к удаленному ресурсу.
    /// </param>
    void Unactive(Exception ex);
}

/// <summary>
/// Интерфейс, определяющий методы контроллера сервиса. <br/>
/// Расширяет методы интерфейса <see cref="IControl{TOptions}"/>
/// </summary>
/// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
/// <typeparam name="TEnvironment">Тип окружения.</typeparam>
public interface IControl<TOptions, TEnvironment> : IControl<TOptions>
    where TOptions : class, new() where TEnvironment : class, new()
{
    /// <summary>Экземпляр окружения.</summary>
    TEnvironment Environment { get; internal set; }
}
