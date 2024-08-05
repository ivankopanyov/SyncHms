namespace SyncHms.Bus.EntityFramework.Models;

public class Exchange
{
    public virtual string Name { get; set; }

    public virtual ICollection<Queue> Queues { get; set; } = [];
}

internal class Exchange<T>(IBusContextFactory busContextFactory,
    EntityFrameworkBusOptions options, ILogger logger) : BusEntity
{
    private readonly HashSet<Queue<T>> _queues = [];

    public override string Name { get; } = GetName(typeof(T));

    public Queue<T, TQueue> Subscribe<TQueue>(Func<T, IMessageContext, Task> handleMessage)
    {
        var queue = new Queue<T, TQueue>(busContextFactory, options, handleMessage, logger);
        _queues.Add(queue);
        return queue;
    }

    public void CreateExchange()
    {
        using var context = busContextFactory.Create();
        if (context.Exchanges.Any(q => q.Name.Equals(Name)))
            return;
        
        context.Exchanges.Add(new Exchange
        {
            Name = Name
        });

        context.SaveChanges();
    }

    public async Task<Dictionary<string, Queue<T>>> PublishAsync(T message)
    {
        if (JsonConvert.SerializeObject(message, options.JsonSerializerSettings) is not {} json)
            throw new SerializationException();

        logger.LogInformation($"{typeof(T).Name}: {json}");
        Dictionary<string, Queue<T>> queues = [];
        foreach (var queue in _queues)
            queues.TryAdd(await queue.PublishAsync(json), queue);

        return queues;
    }
}