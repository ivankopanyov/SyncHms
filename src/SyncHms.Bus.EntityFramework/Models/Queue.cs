namespace SyncHms.Bus.EntityFramework.Models;

/// <summary>Класс, описывающий модель очереди в шине данных.</summary>
public class Queue
{
    /// <summary>Имя очереди.</summary>
    public string Name { get; set; }

    /// <summary>Имя хранилища, из которого перенаправляются сообщения в очередь.</summary>
    public string ExchangeName {  get; set; }

    /// <summary>Экземпляр хранилища, из которого перенаправляются сообщения в очередь.</summary>
    public virtual Exchange Exchange { get; set; }

    /// <summary>Экземпляр сообщений, перенаправленных из хранилища в очередь.</summary>
    public virtual ICollection<Message> Messages { get; set; } = [];
}

/// <summary>Абстрактный класс, описывающий базовую очередь сообщений шины данных.</summary>
/// <typeparam name="TExchange">Тип сообщения.</typeparam>
internal abstract class Queue<TExchange> : BusEntity
{
    /// <summary>
    /// Имя хранилища сообщений, из которого перенаправляются сообщения в очередь.<br/>
    /// Определяется вызовом метода <see cref="BusEntity.GetName"/> с параметром <see cref="TExchange"/>
    /// </summary>
    protected string ExchangeName { get; } = GetName(typeof(TExchange));

    /// <summary>Абстрактный метод, публикующий сообщения в очереди.</summary>
    /// <param name="json">Тело сообщения в формате <c>JSON</c>.</param>
    /// <returns>Идентификатор опубликованного сообщения.</returns>
    public abstract Task<string> PublishAsync(string json);

    /// <summary>Абстрактный метод, вызывающий метод обработки сообщения.</summary>
    /// <param name="messageId">Идентификатор сообщения, которое должно быть обработано.</param>
    /// <returns>
    /// Возвращает новый идентификатор сообщения, если был
    /// вызван метод, возвращающий сообщение обратно в очередь.
    /// </returns>
    public abstract Task<string?> HandleAsync(string messageId);
}

/// <summary>Класс, описывающий очередь сообщений шины данных.</summary>
/// <typeparam name="TExchange">Тип сообщения.</typeparam>
/// <typeparam name="T">Тип потребителя.</typeparam>
/// <param name="busContextFactory">Экземпляр фабрики, предоставляющей контекст базы данных шины.</param>
/// <param name="options">Экземпляр опций шины данных.</param>
/// <param name="handleMessage">Функция, которая будет вызываться при получении сообщения.</param>
/// <param name="logger">Экземпляр логгера.</param>
internal class Queue<TExchange, T>(IBusContextFactory busContextFactory,
    EntityFrameworkBusOptions options, Func<TExchange, IMessageContext, Task> handleMessage,
    ILogger logger) : Queue<TExchange>
{
    /// <summary>
    /// Имя очереди сообщений.<br/>
    /// Определяется вызовом метода <see cref="BusEntity.GetName"/> с параметром <see cref="T"/>
    /// </summary>
    public override string Name { get; } = GetName(typeof(T));

    /// <summary>Абстрактный метод, публикующий сообщения в очереди.</summary>
    /// <param name="json">Тело сообщения в формате <c>JSON</c>.</param>
    /// <returns>Идентификатор опубликованного сообщения.</returns>
    public override async Task<string> PublishAsync(string json)
    {
        var entity = new Message
        {
            Json = json,
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

    /// <summary>Метод, создающий очередь сообщений в базе данных шины, если она не была создана.</summary>
    /// <returns>
    /// Если очередь уже была создана и содержит сообщения,
    /// возвращается дата и время последнего опубликованного в очереди сообщения.
    /// </returns>
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

    /// <summary>
    /// Метод запрашивает в базе данных шины идентификаторы сообщений,
    /// хранящихся в очереди, опубликованных до указанной даты и времени.
    /// </summary>
    /// <param name="max">Максимальная дата и время сообщений, запрашиваемых в базе данных шины.</param>
    /// <returns>Идентификаторы запрашиваемых сообщений.</returns>
    public async Task<List<string>> GetMessageIds(DateTime max)
    {
        await using var context = busContextFactory.Create();
        return await (from m in context.Messages
                where m.ExchangeName.Equals(ExchangeName) && m.QueueName.Equals(Name)
                orderby m.DateTime
                where m.DateTime <= max
                select m.Id).ToListAsync();
    }


    /// <summary>Метод, вызывающий метод обработки сообщения.</summary>
    /// <param name="messageId">Идентификатор сообщения, которое должно быть обработано.</param>
    /// <returns>
    /// Возвращает новый идентификатор сообщения, если был
    /// вызван метод, возвращающий сообщение обратно в очередь.
    /// </returns>
    public override async Task<string?> HandleAsync(string messageId)
    {
        if (await BeginHandleAsync(messageId) is not { } result)
            return null;

        var message = result.Item1;
        var body = result.Item2;

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

    /// <summary>
    /// Метод, вызываемый перед обработкой сообщения.<br/>
    /// Ищет сообщение в базе данных шины и десериализует тело сообщения.
    /// </summary>
    /// <param name="messageId">Идентификатор сообщения.</param>
    /// <returns>Возвращает кортеж с объектом сообщения и десериализованным телом сообщения.</returns>
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

    /// <summary>
    /// Метод, вызываемый после обработкой сообщения.<br/>
    /// Удаляет обработанноесообщение из базы данных шины.<br/>
    /// При необходимости публикует сообщение повторно.
    /// </summary>
    /// <param name="message">Обработанное сообщение.</param>
    /// <param name="body">Тело обработанного сообщения.</param>
    /// <param name="messageContext">Контекст обработки сообщения.</param>
    /// <returns>
    /// Возвращает новый идентификатор сообщения, если был
    /// вызван метод, возвращающий сообщение обратно в очередь.
    /// </returns>
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
