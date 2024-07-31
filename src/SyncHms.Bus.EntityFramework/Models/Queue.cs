namespace SyncHms.Bus.EntityFramework.Models;

public class Queue
{
    public string Name { get; set; }

    public string ExchangeName {  get; set; }

    public virtual Exchange Exchange { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = [];
}

internal abstract class Queue<TExchange>() : BusEntity
{
    protected string ExchangeName { get; } = GetName(typeof(TExchange));

    public abstract Task PublishAsync(string json);
}

internal class Queue<TExchange, T>(IBusContextFactory busContextFactory,
    EntityFrameworkBusOptions options,
    Func<TExchange, IMessageContext, Task> handleMessage) : Queue<TExchange>()
{
    private readonly SemaphoreSlim _semaphore = new(10);

    public override string Name { get; } = GetName(typeof(T));

    public override async Task PublishAsync(string message)
    {
        await using var context = busContextFactory.Create();
        var entity = new Message
        {
            Json = message,
            ExchangeName = ExchangeName,
            QueueName = Name
        };
        await context.Messages.AddAsync(entity);
        await context.SaveChangesAsync();
        Handle(entity.Id);
    }

    public void CreateQueue()
    {
        using var context = busContextFactory.Create();
        var query = from q in context.Queues
            where q.Name.Equals(Name) && q.ExchangeName.Equals(ExchangeName)
            select new { Max = q.Messages.OrderBy(m => m.DateTime).LastOrDefault() };
        
        if (query.FirstOrDefault() is not {} queue)
        {
            context.Queues.Add(new Queue
            {
                Name = Name,
                ExchangeName = ExchangeName
            });
        
            context.SaveChanges();
        }
        else if (queue.Max?.DateTime is {} max)
        {
            GetNext(null, max);
        }
    }

    private void GetNext(DateTime? min, DateTime max)
    {
        async void Start()
        {
            await _semaphore.WaitAsync();

            try
            {
                await using var context = busContextFactory.Create();
                var message = await (from m in context.Messages where m.ExchangeName.Equals(ExchangeName) && m.QueueName.Equals(Name) orderby m.DateTime where (min == null || m.DateTime > min) && m.DateTime <= max select new { m.Id, m.DateTime }).FirstOrDefaultAsync();

                if (message == null) return;

                Handle(message.Id);
                GetNext(message.DateTime, max);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        new Thread(Start).Start();
    }

    private void Handle(string id)
    {
        async void Start()
        {
            await _semaphore.WaitAsync();

            try
            {
                await using var context = busContextFactory.Create();
                if (await (from m in context.Messages
                        where m.Id.Equals(id)
                        select m).FirstOrDefaultAsync() is not { } message)
                    return;

                Message? newMessage = null;

                try
                {
                    if (JsonConvert.DeserializeObject<TExchange>(message.Json, options.JsonSerializerSettings) is not { } body)
                    {
                        context.Messages.Remove(message);
                        await context.SaveChangesAsync();
                        return;
                    }

                    var messageContext = new MessageContext();
                    await handleMessage.Invoke(body, messageContext);

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
                }
                catch
                {
                    return;
                }
                finally
                {
                    context.Messages.Remove(message);
                    await context.SaveChangesAsync();
                }

                if (newMessage != null) Handle(newMessage.Id);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        new Thread(Start).Start();
    }
}

