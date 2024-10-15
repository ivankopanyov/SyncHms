namespace SyncHms.Bus.Events;

/// <summary>Класс, описывающий модель события, передаваемого в шине данных.</summary>
public class Event
{
    /// <summary>Идентификатор задачи.</summary>
    [JsonProperty]
    internal string TaskId { get; set; }

    /// <summary>Имя задачи.</summary>
    [JsonProperty]
    internal string? TaskName { get; set; }

    /// <summary>Флаг, указывающий, что событие является сервисным.</summary>
    [JsonProperty]
    public bool Service { get; set; }

    /// <summary>Сообщение об ошибке.</summary>
    [JsonProperty]
    internal string? Error { get; set; }

    [JsonProperty]
    internal string? StackTrace { get; set; }

    /// <summary>Инициализация события.</summary>
    internal Event()
    {
    }

    /// <summary>Метод, публикующий событие в шине данных.</summary>
    /// <param name="provider">Экземпляр провайдера шины данных.</param>
    internal virtual async Task PublishAsync(IBusProvider provider)
    {
        await provider.PublishAsync(this);
    }
}

/// <summary>
/// Класс, описывающий модель события с сообщением определенного типа, передаваемого в шине данных.<br/>
/// Унаследован от класса <see cref="Event"/>
/// </summary>
public class Event<T> : Event
{
    /// <summary>Экземпляр сообщения события.</summary>
    [JsonProperty]
    internal T Message { get; set; }

    /// <summary>Инициализация события.</summary>
    internal Event()
    {
    }


    /// <summary>Метод, публикующий событие в шине данных.</summary>
    /// <param name="provider">Экземпляр провайдера шины данных.</param>
    internal override async Task PublishAsync(IBusProvider provider)
    {
        await provider.PublishAsync(this);
    }
}
