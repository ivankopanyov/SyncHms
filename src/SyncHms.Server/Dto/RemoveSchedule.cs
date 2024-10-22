namespace SyncHms.Server.Dto;

/// <summary>Класс, описывающий модель объекта передачи сообщения об удалении планируемого события.</summary>
public class RemoveSchedule
{
    /// <summary>Имя планируемого события.</summary>
    public string ScheduleName { get; set; }
}