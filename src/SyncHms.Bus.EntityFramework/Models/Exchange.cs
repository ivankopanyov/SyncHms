namespace SyncHms.Bus.EntityFramework.Models;

/// <summary>Класс, описывающий модель хранилища сообщений шины данных.</summary>
public class Exchange
{
    /// <summary>
    /// Имя хранилища сообщений.
    /// Используется в качестве первичного ключа.
    /// </summary>
    public virtual string Name { get; set; }

    /// <summary>Коллекция объектов сообщений, переданные в хранилище.</summary>
    public virtual ICollection<Queue> Queues { get; set; } = [];
}

/// <summary>Класс, описывающий хранилище сообщений шины данных определенного типа.</summary>
/// <typeparam name="T">Тип сообщения.</typeparam>
/// <param name="busContextFactory">Экземпляр фабрики, предоставляющей контекст базы данных шины.</param>
/// <param name="options">Экземпляр опций шины данных.</param>
/// <param name="logger">Экземпляр логгера.</param>
internal class Exchange<T>(IBusContextFactory busContextFactory,
    EntityFrameworkBusOptions options, ILogger logger) : BusEntity
{
    /// <summary>Коллекция очередей сообщений, в которые перенаправляются сообщения из хранилища.</summary>
    private readonly HashSet<Queue<T>> _queues = [];

    /// <summary>
    /// Имя хранилища сообщений.
    /// Определяется вызовом метода <see cref="BusEntity.GetName"/> с параметром <see cref="T"/>
    /// </summary>
    public override string Name { get; } = GetName(typeof(T));

    /// <summary>Метод, создающий очередь сообщений для переданного обработчика.</summary>
    /// <typeparam name="TQueue">Тип потребителя сообщений.</typeparam>
    /// <param name="handleMessage">Функция, которая будет вызываться при получении сообщения.</param>
    /// <returns>
    /// Экземпляр очереди, в которую будут перенапрявляться
    /// сообщения из хранилища для переданного обработчика.
    /// </returns>
    public Queue<T, TQueue> Subscribe<TQueue>(Func<T, IMessageContext, Task> handleMessage)
    {
        var queue = new Queue<T, TQueue>(busContextFactory, options, handleMessage, logger);
        _queues.Add(queue);
        return queue;
    }

    /// <summary>Метод создает хранилище сообщений в базе данных шины, если оно не было создано.</summary>
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

    /// <summary>Метод, публикующий сообщение в хранилище.</summary>
    /// <param name="message">Экземпляр сообщения.</param>
    /// <returns>
    /// Экземпляры очередей, в которые было перенаправлено сообщение.
    /// В качестве ключа используется имя очереди.
    /// </returns>
    public async Task<Dictionary<string, Queue<T>>?> PublishAsync(T message)
    {
        string json;
        try
        {
            if ((json = JsonConvert.SerializeObject(message, options.JsonSerializerSettings)) == null)
                return null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return null;
        }

        logger.LogInformation($"{typeof(T).Name}: {json}");
        Dictionary<string, Queue<T>> queues = [];
        foreach (var queue in _queues)
            queues.TryAdd(await queue.PublishAsync(json), queue);

        return queues;
    }
}
