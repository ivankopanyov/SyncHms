namespace SyncHms.Bus.Events.Services.Implement;

/// <summary>Класс, описывающий издетеля, публикующего события в шину данных.</summary>
/// <typeparam name="TIn">Тип сообщения.</typeparam>
internal class EventPublisher<TIn> : IEventPublisher<TIn>
{
    /// <summary>Событие, вызываемое при публикации в шину данных.</summary>
    public event PublishHandleAsync<TIn>? PublishEvent;

    /// <summary>Метод, публикующий событие в шину данных.</summary>
    /// <param name="in">Экземпляр сообщения.</param>
    public void Publish(TIn @in) => PublishEvent?.Invoke(@in);
}
