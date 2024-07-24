namespace SyncHms.Bus.EntityFramework;

public class EntityFrameworkBusProvider : IBusProvider<EntityFrameworkBusOptions>
{
    private readonly EntityFrameworkBusOptions _options;
    
    private readonly HashSet<ExchangeHandler> _handlers = [];

    private readonly object _lock = new();

    public EntityFrameworkBusProvider(EntityFrameworkBusOptions options)
    {
        _options = options;
        using var context = new EntityFrameworkBusContext(options);
        context.Database.EnsureCreated();
    }

    public async Task PublishAsync<TMessage>(TMessage message)
    {
        ArgumentNullException.ThrowIfNull(message, nameof(message)); 
        
        var name = GetName(typeof(TMessage));
        if (_handlers.FirstOrDefault(h => h.ExchangeName.Equals(name)) is ExchangeHandler<TMessage> handler)
            await handler.PublishAsync(message);
    }

    public async Task SubscribeAsync<TMessage, TConsumer>(Func<TMessage, MessageContext, Task> handleMessage)
    {
        ArgumentNullException.ThrowIfNull(handleMessage, nameof(handleMessage));

        var name = GetName(typeof(TMessage));
        var exists = true;
        ExchangeHandler<TMessage>? handler;
            
        lock (_lock)
        {
            if ((handler = _handlers.FirstOrDefault(h => h.ExchangeName.Equals(name)) as ExchangeHandler<TMessage>) == null)
            {
                exists = false;
                handler = new ExchangeHandler<TMessage>(_options);
                _handlers.Add(handler);
            }
        }

        if (!exists)
            await handler.CreateExchangeAsync();
        
        await handler.SubscribeAsync<TConsumer>(handleMessage);
    }

    public static string GetName(Type type) => type.AssemblyQualifiedName ?? type.FullName ?? type.Name;
}
