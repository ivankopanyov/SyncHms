namespace SyncHms.Domain;

/// <summary>Класс, описывающий модель сущности, хранящей информацию о сервисе.</summary>
public class Service
{
    /// <summary>Имя сервиса.</summary>
    public string Name { get; set; }

    /// <summary>Опции сервиса в формате <c>JSON</c></summary>
    public string? JsonOptions { get; set; }
    
    /// <summary>Состояние подключения сервиса к удаленному ресурсу.</summary>
    public State? State { get; set; }
}
