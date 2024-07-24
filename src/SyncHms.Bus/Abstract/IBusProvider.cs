namespace SyncHms.Bus;

public interface IBusProvider
{
    Task PublishAsync<TMessage>(TMessage message);

    Task SubscribeAsync<TMessage, TConsumer>(Func<TMessage, MessageContext, Task> handleMessage);
}

public interface IBusProvider<TOptions> : IBusProvider where TOptions : class, new()
{
}
