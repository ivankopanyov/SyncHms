namespace SyncHms.Domain.EntityFramework.Models;

/// <summary>Класс, описывающий модель опций приложения.</summary>
public class AppOptions
{
    /// <summary>Идентификатор опций.</summary>
    public string Id { get; set; }

    /// <summary>Значение опций в формате <c>JSON</c></summary>
    public string Value { get; set; }
}