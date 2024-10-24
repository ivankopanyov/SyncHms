namespace SyncHms.Server.Options;

/// <summary>Класс, описывающий модель опций планировщика событий.</summary>
public class SchedulerOptions
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "Scheduler";

    /// <summary>Коллекция опций событий планировщика.</summary>
    public List<SchedulerItemOptions>? Schedules { get; set; }
}
