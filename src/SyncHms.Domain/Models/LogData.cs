namespace SyncHms.Domain;

/// <summary>Класс, описывающий модель сущности, хранящей данные лога.</summary>
public class LogData
{
    /// <summary>
    /// Идентификатор данных лога.
    /// Игнорируется при сериализации.
    /// </summary>
    [JsonIgnore]
    public string Id { get; set; }
    
    /// <summary>Идентификатор лога, к которому относятся данные.</summary>
    public string LogId { get; set; }
    
    /// <summary>Идентификатор задачи.</summary>
    public string TaskId { get; set; }
    
    /// <summary>Сообщение об ошибке, возникшей во время обработки события.</summary>
    public string? Error { get; set; }

    public string? StackTrace { get; set; }

    /// <summary>Сообщение обрабатываемого события в формате <c>JSON</c></summary>
    public string? InputObjectJson { get; set; }
    
    /// <summary>Сущность лога, к которому относятся данные.</summary>
    public virtual Log Log { get; set; }
}