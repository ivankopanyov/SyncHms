namespace SyncHms.Domain;

/// <summary>Класс, описывающий модель состояния подключения сервиса к удаленному ресурсу.</summary>
public class State
{
    /// <summary>Флаг, указывающий активно ли в данный момент подключение сервиса.</summary>
    public bool IsActive { get; set; }

    /// <summary>Текст ошибки, возникшей в момент подключения.</summary>
    public string? Error { get; set; }

    public string? StackTrace { get; set; }

    /// <summary>Текст ошибки, возникшей, если не удалось применить новый опции сервиса.</summary>
    public string? Info { get; set; }
}