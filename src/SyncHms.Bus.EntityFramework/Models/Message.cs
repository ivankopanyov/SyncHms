namespace SyncHms.Bus.EntityFramework.Models;

/// <summary>Класс, описывающий модель сообщения шины данных.</summary>
public class Message
{
    /// <summary>Идентификатор сообщения.</summary>
    public string Id { get; set; }

    /// <summary>Имя хранилища, в которое передано сообщение.</summary>
    public string ExchangeName { get; set; }

    /// <summary>Имя очереди, в которую передано сообщение.</summary>
    public string QueueName { get; set; }

    /// <summary>Дата и время публикации сообщения.</summary>
    public DateTime DateTime { get; set; } = DateTime.Now;

    /// <summary>Тело сообщения в формате <c>JSON</c>.</summary>
    public string Json { get; set; }

    /// <summary>Экземпляр очереди, в которую передано сообщение.</summary>
    public virtual Queue Queue { get; set; }
}
