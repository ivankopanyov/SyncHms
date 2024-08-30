namespace SyncHms.Bus.Services;

/// <summary>Класс, описывающий модель опций сервиса.</summary>
public class ServiceOptions
{
    /// <summary>Имя сервиса.</summary>
    public string ServiceName { get; set; }
}

/// <summary>
/// Класс, описывающий модель опций сервиса.<br/>
/// Унаследован от класса <see cref="ServiceOptions"/>
/// </summary>
/// <typeparam name="TOptions">
/// Тип опций сервиса.
/// Должен содержать открытый конструктор без параметров.
/// </typeparam>
public class ServiceOptions<TOptions> : ServiceOptions where TOptions : class, new()
{
}
