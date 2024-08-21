namespace SyncHms.Bus.Services.Services.Implement;

/// <summary>
/// Класс, описывающий контроллер сервиса.<br/>
/// Реализует интерфейс <see cref="IControl{TOptions}"/>
/// </summary>
/// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
/// <param name="options">Экземпляр опций сервиса.</param>
internal class Control<TOptions>(IOptions<TOptions>? options) : IControl<TOptions> where TOptions : class, new()
{
    /// <summary>Поле, хранящее текущие опции сервиса.</summary>
    private TOptions _options = options?.Value ?? new();

    /// <summary>Текущие опции сервиса.</summary>
    public TOptions Options
    {
        get => _options;
        set => _options = value ?? new();
    }

    /// <summary>
    /// Событие, вызываемое при получении сообщения об удачном
    /// подключении сервиса к удаленному ресурсу.
    /// </summary>
    public event ActiveHandleAsync? ActiveEvent;

    /// <summary>
    /// Событие, вызываемое при отправке сообщения о неудачной
    /// попытке подключении сервиса к удаленному ресурсу.
    /// </summary>
    public event UnactiveHandleAsync? UnactiveEvent;

    /// <summary>Метод, вызывающий событие <see cref="ActiveEvent"/></summary>
    public void Active() => ActiveEvent?.Invoke();

    /// <summary>Метод, вызывающий событие <see cref="UnactiveEvent"/></summary>
    /// <param name="error">
    /// Сообщение об ошибке, вызванной во время подключения сервиса к удаленному ресурсу.
    /// </param>
    public void Unactive(string error) => UnactiveEvent?.Invoke(error);

    /// <summary>Метод, вызывающий событие <see cref="UnactiveEvent"/></summary>
    /// <param name="ex">
    /// Исключение, возбужденное во время подключения сервиса к удаленному ресурсу.
    /// </param>
    public void Unactive(Exception ex) => UnactiveEvent?.Invoke(ex.Message, ex);
}

/// <summary>
/// Класс, описывающий контроллер сервиса.<br/>
/// Унаследован от класса <see cref="Control{TOptions}"/><br/>
/// Реализует интерфейс <see cref="IControl{TOptions}"/>
/// </summary>
/// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
/// <typeparam name="TEnvironment">Тип окружения.</typeparam>
/// <param name="options">Экземпляр опций сервиса.</param>
internal class Control<TOptions, TEnvironment>(IOptions<TOptions>? options) :
    Control<TOptions>(options), IControl<TOptions, TEnvironment>
    where TOptions : class, new() where TEnvironment : class, new()
{
    /// <summary>Поле, хранящее окружение.</summary>
    private TEnvironment _environment = new();

    /// <summary>Окружение.</summary>
    public TEnvironment Environment
    {
        get => _environment;
        set => _environment = value ?? new();
    }
}
