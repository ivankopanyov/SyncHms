namespace SyncHms.Bus.EntityFramework.Repositories.Abstract;

internal interface IExchangeRepository
{
    void Add<TMessage, TConsumer>(Func<TMessage, IMessageContext, Task> handleMessage);

    Task AddMessageAsync<TMessage>(TMessage message);
}
