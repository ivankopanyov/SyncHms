namespace SyncHms.Bus.EntityFramework.Infrastructure;

internal abstract class ExchangeHandler()
{
    public abstract string ExchangeName { get; }

    public override int GetHashCode() => ExchangeName.GetHashCode();

    public override bool Equals(object? obj) => obj is ExchangeHandler other && ExchangeName.Equals(other.ExchangeName);
}

internal class ExchangeHandler<TExchange>(EntityFrameworkBusOptions options) : ExchangeHandler()
{
    private readonly HashSet<MessageHandler<TExchange>> _handlers = [];

    public override string ExchangeName => EntityFrameworkBusProvider.GetName(typeof(TExchange));

    public async Task SubscribeAsync<TQueue>(Func<TExchange, MessageContext, Task> handleMessage)
    {
        var handler = new MessageHandler<TExchange, TQueue>(options, handleMessage);
        await handler.CreateQueueAsync();
        _handlers.Add(handler);
    }

    public async Task CreateExchangeAsync()
    {
        var exchangeName = EntityFrameworkBusProvider.GetName(typeof(TExchange));
        
        await using var context = new EntityFrameworkBusContext(options);
        if (!await context.Exchanges.AnyAsync(q => q.Name.Equals(exchangeName)))
        {
            await context.Exchanges.AddAsync(new()
            {
                Name = exchangeName
            });

            await context.SaveChangesAsync();
        }
    }

    public async Task PublishAsync(TExchange message)
    {
        if (JsonConvert.SerializeObject(message, options.JsonSerializerSettings) is not {} json)
            throw new SerializationException();
        
        foreach (var handler in _handlers)
            await handler.PublishAsync(json);
    }
}
