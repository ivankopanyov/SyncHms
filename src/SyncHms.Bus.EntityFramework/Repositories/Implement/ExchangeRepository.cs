namespace SyncHms.Bus.EntityFramework.Repositories.Implement;

internal class ExchangeRepository(IBusContextFactory busContextFactory,
    EntityFrameworkBusOptions options): IExchangeRepository
{
    private readonly HashSet<BusEntity> _exchanges = [];

    private readonly object _lock = new();

    public void Add<TMessage, TConsumer>(Func<TMessage, IMessageContext, Task> handleMessage)
    {
        ArgumentNullException.ThrowIfNull(handleMessage, nameof(handleMessage));

        var name = BusEntity.GetName(typeof(TMessage));
        var exists = true;
        Exchange<TMessage>? exchange;
            
        lock (_lock)
        {
            if ((exchange = _exchanges.FirstOrDefault(h => h.Name.Equals(name)) as Exchange<TMessage>) == null)
            {
                exists = false;
                exchange = new Exchange<TMessage>(busContextFactory, options);
                _exchanges.Add(exchange);
            }
        }

        if (!exists)
            exchange.CreateExchange();
        
        exchange.Subscribe<TConsumer>(handleMessage);
    }
    
    public async Task AddMessageAsync<TMessage>(TMessage message)
    {
        ArgumentNullException.ThrowIfNull(message, nameof(message)); 
        
        var name = BusEntity.GetName(typeof(TMessage));
        if (_exchanges.FirstOrDefault(h => h.Name.Equals(name)) is Exchange<TMessage> exchange)
            await exchange.PublishAsync(message);
    }
}
