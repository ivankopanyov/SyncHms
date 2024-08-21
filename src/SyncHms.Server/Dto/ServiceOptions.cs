namespace SyncHms.Server.Dto;

/// <summary>Класс, описывающий модель объекта передачи опций сервиса.</summary>
public class ServiceSettings
{
    /// <summary>Опции сервиса в формате <c>JSON</c></summary>
    [Required]
    public string JsonOptions { get; set; }
}
