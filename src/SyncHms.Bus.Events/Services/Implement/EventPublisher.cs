namespace SyncHms.Bus.Events.Services.Implement;

internal class EventPublisher<TIn> : IEventPublisher<TIn>
{
    public event PublishHandleAsync<TIn>? PublishEvent;

    public void Publish(TIn @in) => PublishEvent?.Invoke(@in);
}
