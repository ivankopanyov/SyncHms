namespace SyncHms.Bus.Events;

/// <summary>Класс, описывабщий модель опции планируемого события.</summary>
/// <param name="scheduleName">Имя планируемого события.</param>
/// <param name="description">Описание планируемого события.</param>
public class ScheduleOptions(string scheduleName, string? description = null)
{
    /// <summary>Описание планируемого события.</summary>
    public string? Description { get; } = description;

    /// <summary>Интервал выполнения события.</summary>
    public TimeSpan Interval { get; internal set; } = TimeSpan.Zero;

    /// <summary>Дата и время последней удачной обработки события.</summary>
    public DateTime Last { get; internal set; } = DateTime.Now;

    /// <summary>Ключ события.</summary>
    internal JobKey Key { get; } = new(scheduleName);
}
