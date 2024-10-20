namespace SyncHms.Server.Dto;

/// <summary>Класс, описывающий модель объекта передачи сообщения об изменении опций планируемого события.</summary>
public class UpdateSchedule
{
    /// <summary>Интервал выполнения события.</summary>
    [Range(0, int.MaxValue)]
    public int IntervalSeconds { get; set; }

    /// <summary>Дата и время последней удачной обработки события.</summary>
    public DateTime Last { get; set; }
}
