namespace SyncHms.Bus.EntityFramework.Models;

internal class Exchange
{
    public virtual string Name { get; set; }

    public virtual ICollection<Queue> Queues { get; set; } = [];
}

internal class Exchange<T>(IBusContextFactory busContextFactory,
    EntityFrameworkBusOptions options) : BusEntity
{
    private readonly HashSet<Queue<T>> _queues = [];

    public override string Name { get; } = GetName(typeof(T));

    public void Subscribe<TQueue>(Func<T, IMessageContext, Task> handleMessage)
    {
        var queue = new Queue<T, TQueue>(busContextFactory, options, handleMessage);
        queue.CreateQueue();
        _queues.Add(queue);
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

    public async Task PublishAsync(T message)
    {
        if (JsonConvert.SerializeObject(message, options.JsonSerializerSettings) is not {} json)
            throw new SerializationException();
        
        foreach (var queue in _queues)
            await queue.PublishAsync(json);
    }
}
