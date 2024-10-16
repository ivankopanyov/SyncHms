namespace SyncHms.Events.Messages;

/// <summary>Класс, описывающий событие изменения состояния сервиса.</summary>
public class ChangedServiceState
{
    /// <summary>Имя сервиса.</summary>
    public string ServiceName { get; set; }
    
    /// <summary>Состояние подключения к удаленному сервису.</summary>
    public bool Connection { get; set; }
    
    /// <summary>Сообщение о состоянии сервиса.</summary>
    public string? Message { get; set; }
}
