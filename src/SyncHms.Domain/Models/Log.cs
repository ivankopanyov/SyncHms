namespace SyncHms.Domain;

/// <summary>Класс, описывающий модель лога.</summary>
public class Log
{
    /// <summary>Идентификатор лога.</summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Идентификатор сущности, хранящей данные лога.
    /// Игнорируется при сериализации.
    /// </summary>
    [JsonIgnore]
    public string? LogDataId { get; set; }

    /// <summary>Идентификатор задачи.</summary>
    public string TaskId { get; set; }

    /// <summary>Имя задачи.</summary>
    public string? TaskName { get; set; }

    /// <summary>Имя обработчика.</summary>
    public string? HandlerName { get; set; }

    /// <summary>Дата и время создания лога.</summary>
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime DateTime { get; set; }

    /// <summary>Свойство, указывающее, была ли завершена обработка события с ошибкой.</summary>
    public bool IsError { get; set; }

    /// <summary>Свойство, указывающее, был ли обработчик последним в задаче.</summary>
    public bool IsEnd { get; set; }

    /// <summary>Краткое описание входных данных для обработчика.</summary>
    public string? Message { get; set; }

    /// <summary>Сущность, хранящая данные лога.</summary>
    public virtual LogData? LogData { get; set; }
}
