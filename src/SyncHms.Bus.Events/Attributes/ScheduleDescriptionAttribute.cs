namespace SyncHms.Bus.Events;

/// <summary>Класс, описывающий атрибут, добавляющий описание планируемого события.</summary>
/// <param name="description">Описание планируемого события.</param>
[AttributeUsage(AttributeTargets.Class)]
public class ScheduleDescriptionAttribute(string? description) : Attribute
{
    /// <summary>Описание планируемого события.</summary>
    public string? Description { get; } = description;
}
