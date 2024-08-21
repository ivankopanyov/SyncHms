namespace SyncHms.Bus.EntityFramework.Repositories.Implement;

/// <summary>
/// Класс, описывающий репозиторий хранилищ сообщений шины данных.<br/>
/// Реализует интерфейс <see cref="IExchangeRepository"/>.
/// </summary>
/// <param name="busContextFactory">Экземпляр фабрики, предоставляющей контекст базы данных шины.</param>
/// <param name="options">Экземпляр опций шины данных.</param>
/// <param name="logger">Экземпляр логгера.</param>
internal class ExchangeRepository(IBusContextFactory busContextFactory,
    EntityFrameworkBusOptions options, ILogger<ExchangeRepository> logger): IExchangeRepository
{
    /// <summary>Коллекция хранилищ сообщений шины.</summary>
    private readonly HashSet<BusEntity> _exchanges = [];

    /// <summary>
    /// Объект для блокировки оператором <c>lock</c>.
    /// Исключает дублирование создания хранилищ сообщений определенного типа.
    /// </summary>
    private readonly object _lock = new();

    /// <summary>Устанавливает колличество потоков, одновременно обрабатывающих сообщения из шины данных.</summary>
    private readonly SemaphoreSlim _semaphore = new(10);

    /// <summary>
    /// Метод добавляет обработчик для определённого типа сообщения
    /// и типа потребителя, если он еще не был добавлен.
    /// </summary>
    /// <typeparam name="TMessage">Тип сообщения.</typeparam>
    /// <typeparam name="TConsumer">Тип потребителя.</typeparam>
    /// <param name="handleMessage">Функция, которая будет вызываться при получении сообщения.</param>
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

    /// <summary>Метод добавляет сообщение в очередь.</summary>
    /// <typeparam name="TMessage">Тип сообщения</typeparam>
    /// <param name="message">Экземпляр сообщения.</param>
    public async Task AddMessageAsync<TMessage>(TMessage message)
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
        
        if (await exchange.PublishAsync(message) is { } queues)
            foreach (var queue in queues)
                MessageHandle(queue.Key, queue.Value);
    }

    /// <summary>Метод, вызывающий метод обработки сообщения определенного типа.</summary>
    /// <typeparam name="TExchange">Тип сообщения.</typeparam>
    /// <param name="id">Идентификатор сообщения.</param>
    /// <param name="queue">Экземпляр очереди определенного типа сообщений.</param>
    private void MessageHandle<TExchange>(string id, Models.Queue<TExchange> queue)
    {
        new Thread(StartAsync)
        {
            Priority = ThreadPriority.Lowest
        }.Start();
        return;

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
    }
    
    /// <summary>
    /// Метод загружает в память из хранилища сообщения указанного типа
    /// для указанного потребителя, созданные до указанной даты.
    /// </summary>
    /// <typeparam name="TExchange">Тип сообщения</typeparam>
    /// <typeparam name="TQueue">Тип потребителя.</typeparam>
    /// <param name="queue">Экземпляр очереди сообщений.</param>
    /// <param name="max">Дата, до которой созданы сообщения, загружаемые из базы данных.</param>
    private void LoadMessages<TExchange, TQueue>(Queue<TExchange, TQueue> queue, DateTime max)
    {
        new Thread(StartAsync)
        {
            Priority = ThreadPriority.Lowest
        }.Start();
        return;

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
    }
}
