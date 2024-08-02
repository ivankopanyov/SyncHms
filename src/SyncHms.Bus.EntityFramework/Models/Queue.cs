namespace SyncHms.Bus.EntityFramework.Models;

public class Queue
{
    public string Name { get; set; }

    public string ExchangeName {  get; set; }

    public virtual Exchange Exchange { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = [];
}

internal abstract class Queue<TExchange> : BusEntity
{
    protected string ExchangeName { get; } = GetName(typeof(TExchange));

    public abstract Task<string> PublishAsync(string json, bool fast);

    public abstract Task<string?> HandleAsync(string messageId);
}

internal class Queue<TExchange, T>(IBusContextFactory busContextFactory,
    EntityFrameworkBusOptions options, Func<TExchange, IMessageContext, Task> handleMessage,
    ILogger logger) : Queue<TExchange>
{
    public override string Name { get; } = GetName(typeof(T));

    public override async Task<string> PublishAsync(string message, bool fast)
    {
        var entity = new Message
        {
            Fast = fast,
            Json = message,
            ExchangeName = ExchangeName,
            QueueName = Name
        };

        await using (var context = busContextFactory.Create())
        {
            await context.Messages.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        return entity.Id;
    }

    public DateTime? CreateQueue()
    {
        using var context = busContextFactory.Create();
        var query = from q in context.Queues
            where q.Name.Equals(Name) && q.ExchangeName.Equals(ExchangeName)
            select new { Max = q.Messages.OrderBy(m => m.DateTime).LastOrDefault() };

        if (query.FirstOrDefault() is { } queue) 
            return queue.Max?.DateTime;
        
        context.Queues.Add(new Queue
        {
            Name = Name,
            ExchangeName = ExchangeName
        });

        context.SaveChanges();
        return null;
    }

    public async Task<List<string>> GetMessageIds(DateTime max)
    {
        await using var context = busContextFactory.Create();
        return await (from m in context.Messages
                where m.ExchangeName.Equals(ExchangeName) && m.QueueName.Equals(Name)
                orderby m.DateTime
                where m.DateTime <= max
                select m.Id).ToListAsync();
    }

    public override async Task<string?> HandleAsync(string messageId)
    {
        if (await BeginHandleAsync(messageId) is not { } result)
            return null;

        var message = result.Item1;
        var body = result.Item2;

        // if (!message.Fast)
        //     await Task.Delay(100);

        var messageContext = new MessageContext();
        try
        {
            await handleMessage.Invoke(body, messageContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
        }

        return await EndHandleAsync(message, body, messageContext);
    }

    private async Task<(Message, TExchange)?> BeginHandleAsync(string messageId)
    {
        try
        {
            await using var context = busContextFactory.Create();
            return await context.Messages.AsNoTracking().FirstOrDefaultAsync(m => m.Id.Equals(messageId)) is { } message
                   && JsonConvert.DeserializeObject<TExchange>(message.Json, options.JsonSerializerSettings) is { } body
                ? (message, body) : null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return null;
        }
    }
    
    private async Task<string?> EndHandleAsync(Message message, TExchange body, MessageContext messageContext)
    {
        Message? newMessage = null;
        await using var context = busContextFactory.Create();
        if (messageContext.IsRequeue)
        {
            newMessage = new Message
            {
                Json = messageContext.Update
                    ? JsonConvert.SerializeObject(body, options.JsonSerializerSettings)
                    : message.Json,
                QueueName = message.QueueName,
                ExchangeName = message.ExchangeName
            };

            await context.Messages.AddAsync(newMessage);
        }
        context.Messages.Remove(message);
        await context.SaveChangesAsync();

        return newMessage?.Id;
    }
}
