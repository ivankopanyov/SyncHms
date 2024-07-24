namespace SyncHms.Bus.EntityFramework.Infrastructure;

internal abstract class MessageHandler<TExchange>()
{
    protected abstract string QueueName { get; }

    public abstract Task PublishAsync(string json);

    public override int GetHashCode() => QueueName.GetHashCode();

    public override bool Equals(object? obj) => obj is MessageHandler<TExchange> other && QueueName.Equals(other.QueueName);
}

internal class MessageHandler<TExchange, TQueue>(EntityFrameworkBusOptions options,
    Func<TExchange, MessageContext, Task> handleMessage) : MessageHandler<TExchange>()
{
    private readonly string _exchangeName = EntityFrameworkBusProvider.GetName(typeof(TExchange));
    
    private readonly string _queueName = EntityFrameworkBusProvider.GetName(typeof(TQueue));
    
    private readonly SemaphoreSlim _semaphore = new(1);
    
    private readonly SemaphoreSlim _handleSemaphore = new(3);

    protected override string QueueName => _queueName;
    
    public override async Task PublishAsync(string message)
    {
        await using var context = new EntityFrameworkBusContext(options);
        var entity = new Message
        {
            Json = message,
            ExchangeName = _exchangeName,
            QueueName = _queueName
        };
        await context.Messages.AddAsync(entity);

        await _semaphore.WaitAsync();

        try
        {
            await context.SaveChangesAsync();
            Handle(entity.Id);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task CreateQueueAsync()
    {
        await using var context = new EntityFrameworkBusContext(options);
        var query = from q in context.Queues
            where q.Name.Equals(_queueName) && q.ExchangeName.Equals(_exchangeName)
            select new { q.Messages };
        
        if (await query.FirstOrDefaultAsync() is not {} queue)
        {
            await context.Queues.AddAsync(new()
            {
                Name = _queueName,
                ExchangeName = _exchangeName
            });
        
            await context.SaveChangesAsync();
        }
        else if (queue.Messages.Count > 0)
        {
            foreach (var message in queue.Messages)
                message.Handled = false;

            await context.SaveChangesAsync();
            
            foreach (var message in queue.Messages)
                Handle(message.Id);
        }
    }

    private void Handle(string id) => new Thread(async () =>
    {
        await _handleSemaphore.WaitAsync();
        
        try
        {
            Message? message;
            await using var context = new EntityFrameworkBusContext(options);
            await _semaphore.WaitAsync();

            try
            {
                message = await (from m in context.Messages
                    where m.Id.Equals(id) && !m.Handled
                    select m).FirstOrDefaultAsync();

                if (message == null)
                    return;

                message.Handled = true;
                await context.SaveChangesAsync();
            }
            finally
            {
                _semaphore.Release();
            }

            try
            {
                if (JsonConvert.DeserializeObject<TExchange>(message.Json, options.JsonSerializerSettings) is not {} m)
                {
                    context.Messages.Remove(message);
                    await context.SaveChangesAsync();
                    return;
                }

                var messageContext = new MessageContext();
                await handleMessage.Invoke(m, messageContext);
                if (messageContext.IsRequeue)
                {
                    message.Handled = false;
                    await context.SaveChangesAsync();
                    Handle(id);
                    return;
                }
                
                context.Messages.Remove(message);
                await context.SaveChangesAsync();

            }
            catch
            {
                context.Messages.Remove(message);
                await context.SaveChangesAsync();
            }
        }
        finally
        {
            _handleSemaphore.Release();
        }
    }).Start();
}
 