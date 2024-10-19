namespace SyncHms.Bus.Events;

/// <summary>
/// Класс, описывающий модель сообщения, отправляемого в шину данных,
/// содержащего опции обработки планируемого события.
/// </summary>
public class ScheduleEvent
{
    /// <summary>Имя обработчика планируемого события.</summary>
    internal string Destination { get; init; }

    /// <summary>Экземпляр планировщика событий.</summary>
    internal IEventScheduler EventScheduler { get; init; }

    /// <summary>Дата и время последней удачной обработки планируемого события.</summary>
    internal DateTime Previous { get; init; }

    /// <summary>Дата и время текущей обработки планируемого события.</summary>
    internal DateTime Current { get; init; }
}
