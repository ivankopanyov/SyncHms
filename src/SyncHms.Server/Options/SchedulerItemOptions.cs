namespace SyncHms.Server.Options;

/// <summary>Класс, описывающий модель события планировщика.</summary>
public class SchedulerItemOptions
{
    /// <summary>Уникальное имя события.</summary>
    public string Name { get; set; }

    /// <summary>Интервал выполнения события в секундах.</summary>
    public int IntervalSeconds { get; set; }

    /// <summary>Последняя успешная обработка события в секундах.</summary>
    public DateTime Last { get; set; } = DateTime.Now;
}
