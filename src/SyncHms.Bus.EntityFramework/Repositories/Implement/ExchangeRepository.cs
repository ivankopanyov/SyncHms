namespace SyncHms.Bus.EntityFramework.Repositories.Implement;

internal class ExchangeRepository(IBusContextFactory busContextFactory,
    EntityFrameworkBusOptions options, ILogger<ExchangeRepository> logger): IExchangeRepository
{
    private readonly HashSet<BusEntity> _exchanges = [];

    private readonly object _lock = new();

    private readonly SemaphoreSlim _semaphore = new(10);

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
                exchange = new Exchange<TMessage>(busContextFactory, options, logger);
                _exchanges.Add(exchange);
            }
        }

        if (!exists)
            exchange.CreateExchange();

        var queue = exchange.Subscribe<TConsumer>(handleMessage);
        if (queue.CreateQueue() is { } max)
            LoadMessages(queue, max);
    }
    
    public async Task AddMessageAsync<TMessage>(TMessage message, bool fast)
    {
        ArgumentNullException.ThrowIfNull(message, nameof(message)); 
        
        var name = BusEntity.GetName(typeof(TMessage));
        Exchange<TMessage>? exchange;
        lock (_lock)
        {
            exchange = _exchanges.FirstOrDefault(h => h.Name.Equals(name)) as Exchange<TMessage>;
        }

        if (exchange == null)
            return; 
        
        var queues = await exchange.PublishAsync(message, fast);
        foreach (var queue in queues)
            MessageHandle(queue.Key, queue.Value);
    }

    private void MessageHandle<TExchange>(string id, Models.Queue<TExchange> queue)
    {
        async void StartAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (await queue.HandleAsync(id) is { } result)
                    MessageHandle(result, queue);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        new Thread(StartAsync)
        {
            Priority = ThreadPriority.Lowest
        }.Start();
    }
    
    private void LoadMessages<TExchange, TQueue>(Queue<TExchange, TQueue> queue, DateTime max)
    {
        async void StartAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                var ids = await queue.GetMessageIds(max);
                foreach (var id in ids)
                    MessageHandle(id, queue);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        new Thread(StartAsync)
        {
            Priority = ThreadPriority.Lowest
        }.Start();
    }
}
