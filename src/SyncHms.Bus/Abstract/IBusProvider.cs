namespace SyncHms.Bus;

/// <summary>Интерфейс определяет методы для работы с шиной данных.</summary>
public interface IBusProvider
{
    /// <summary>Метод позволяет опубликовать сообщение в шине данных.</summary>
    /// <typeparam name="TMessage">Тип сообщения.</typeparam>
    /// <param name="message">Экземпляр сообщения.</param>
    Task PublishAsync<TMessage>(TMessage message);

    /// <summary>Метод позволяет подписаться на сообщения указанного типа.</summary>
    /// <typeparam name="TMessage">Тип сообщения.</typeparam>
    /// <typeparam name="TConsumer">Тип потребителя сообщений.</typeparam>
    /// <param name="handleMessage">Обработчик сообщения.</param>
    IBusProvider Subscribe<TMessage, TConsumer>(Func<TMessage, IMessageContext, Task> handleMessage);
}

/// <summary>Интерфейс расширяет <see cref="IBusProvider"/>, определяющий методы для работы с шиной данных.</summary>
/// <typeparam name="TOptions">Тип объекта, содержащего опции шины данных.</typeparam>
public interface IBusProvider<TOptions> : IBusProvider where TOptions : class, new()
{
}
