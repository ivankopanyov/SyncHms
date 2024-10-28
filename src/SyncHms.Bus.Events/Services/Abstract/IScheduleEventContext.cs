namespace SyncHms.Bus.Events;

/// <summary>
/// Интерфейс, определяющий методы контекста обработки планируемого события.<br/>
/// Расширяет методы интерфейса <see cref="IEventContext"/>
/// </summary>
public interface IScheduleEventContext : IEventContext
{
    /// <summary>Дата и время последней удачной обработки события.</summary>
    public DateTime Previous { get; }

    /// <summary>Дата и время текущей обработки события.</summary>
    public DateTime Current { get; }
}
