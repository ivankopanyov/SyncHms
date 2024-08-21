namespace SyncHms.Bus.Services;

/// <summary>Класс, описывающий модель сообщения, передающего обновленные опции сервису.</summary>
public class Options
{
    /// <summary>Имя сервиса.</summary>
    public string ServiceName { get; set; }
    
    /// <summary>Опции сервиса в формате <c>JSON</c>.</summary>
    public string? JsonOptions { get; set; }
}
