namespace SyncHms.Bus.EntityFramework.Implement;

internal class EntityFrameworkBusProvider(IExchangeRepository repository)
    : IBusProvider<EntityFrameworkBusOptions>
{
    public async Task PublishAsync<TMessage>(TMessage message) =>
        await repository.AddMessageAsync(message);

    public IBusProvider Subscribe<TMessage, TConsumer>(Func<TMessage, IMessageContext, Task> handleMessage)
    {
        repository.Add<TMessage, TConsumer>(handleMessage);
        return this;
    }
}
