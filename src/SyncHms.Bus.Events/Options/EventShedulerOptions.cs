namespace SyncHms.Bus.Events.Options;

/// <summary>Класс, описывающий модель опций планировщика событий <see cref="IEventScheduler"/>.</summary>
internal class EventShedulerOptions
{
    /// <summary>Коллекция опций планируемых событий с именем события в качестве ключа.</summary>
    public Dictionary<string, ScheduleOptions> Events { get; } = [];
}
