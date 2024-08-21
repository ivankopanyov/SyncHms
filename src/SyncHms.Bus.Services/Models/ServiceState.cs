namespace SyncHms.Bus.Services;

/// <summary>
/// Класс, описывающий текущее состояние подключения сервиса к удаленному ресурсу.
/// </summary>
public class ServiceState
{
    /// <summary>Свойство, хранящее текущее состояние подключения.</summary>
    public bool IsActive { get; set; }

    /// <summary>Текст ошибки, возникшей в ходе подключения.</summary>
    public string? Error { get; set; }

    public string? StackTrace { get; set; }

    /// <summary>
    /// Текст ошибки, возникшей при попытке применить обновленные опции к сервису.<br/>
    /// Ошибка может возникуть при неудачной попытке десериализации опций.
    /// </summary>
    public string? Info { get; set; }
}
