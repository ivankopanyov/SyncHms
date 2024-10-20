namespace SyncHms.Server.Dto;

/// <summary>Модель, хранящая информацию о планируемом событии.</summary>
public class ScheduleInfo
{
    /// <summary>Имя планируемого события.</summary>
    public string Name { get; set; }

    /// <summary>Описание планируемого события.</summary>
    public string? Description { get; set; }

    /// <summary>Интервал выполнения события в секундах.</summary>
    public int IntervalSeconds { get; set; }

    /// <summary>Дата и время последней удачной обработки события.</summary>
    public DateTime Last { get; set; }

    /// <summary>Сообщение об ошибке.</summary>
    public string? Message { get; internal set; }

    public string? StackTrace { get; internal set; }
}
