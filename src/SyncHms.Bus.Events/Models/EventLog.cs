namespace SyncHms.Bus.Events;

/// <summary>Класс, описывающий модель логов, отправляемых обработчиками событий.</summary>
public class EventLog
{
    /// <summary>Идентификатор задачи.</summary>
    public string TaskId { get; set; }

    /// <summary>Имя задачи.</summary>
    public string? TaskName { get; set; }

    /// <summary>Имя обработчика.</summary>
    public string? HandlerName { get; set; }

    /// <summary>Дата и время публикации события.</summary>
    public DateTime DateTime { get; set; } = DateTime.Now.Trim(TimeSpan.TicksPerMillisecond);

    /// <summary>Свойство, указывающее, было ли завершено выполнение задачи.</summary>
    public bool IsEnd { get; set; }

    /// <summary>Описание события.</summary>
    public string? Message { get; set; }
    
    /// <summary>Сообщение об ошибке.</summary>
    public string? Error { get; set; }

    public string? StackTrace { get; set; }

    /// <summary>Сообщение, переданное на выполнение обработчику в формате <c>JSON</c>.</summary>
    public string? InputObjectJson { get; set; }
}
