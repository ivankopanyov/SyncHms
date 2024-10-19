namespace SyncHms.Bus.Events.Options;

/// <summary>Класс, описывабщий модель опции планируемого события.</summary>
/// <param name="handlerName">Имя планируемого события.</param>
/// <param name="description">Описание планируемого события.</param>
internal class ScheduleOptions(string handlerName, string? description = null)
{
    /// <summary>Описание планируемого события.</summary>
    internal string? Description { get; init; } = description;

    /// <summary>Интервал выполнения события.</summary>
    public TimeSpan Interval { get; set; } = TimeSpan.Zero;

    /// <summary>Дата и время последней удачной обработки события.</summary>
    public DateTime Last { get; set; } = DateTime.Now;

    /// <summary>Ключ события.</summary>
    public JobKey Key { get; } = new JobKey(handlerName);
}
