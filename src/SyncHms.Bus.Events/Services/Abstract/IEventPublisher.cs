namespace SyncHms.Bus.Events;

internal delegate Task PublishHandleAsync<in TIn>(TIn @in);

public interface IEventPublisher<TIn>
{
    internal event PublishHandleAsync<TIn>? PublishEvent;

    void Publish(TIn @in);
}
