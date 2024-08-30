namespace SyncHms.Bus.EntityFramework.Implement;

/// <summary>
/// Класс реализует интерфейс <see cref="IBusProvider{TOptions}"/>, определяющий методы для работы с шиной данных.<br/>
/// В основе работы использует реляционную базу данных, взаимодействуя с ней с помощью <c>EntityFramework</c>.
/// </summary>
/// <param name="repository">Экземпляр репозитория для работы с хранилищами сообщений шины данных.</param>
internal class EntityFrameworkBusProvider(IExchangeRepository repository)
    : IBusProvider<EntityFrameworkBusOptions>
{

    /// <summary>Метод позволяет опубликовать сообщение в шине данных.</summary>
    /// <typeparam name="TMessage">Тип сообщения.</typeparam>
    /// <param name="message">Экземпляр сообщения.</param>
    public async Task PublishAsync<TMessage>(TMessage message) =>
        await repository.AddMessageAsync(message);

    /// <summary>Метод позволяет подписаться на сообщения указанного типа.</summary>
    /// <typeparam name="TMessage">Тип сообщения.</typeparam>
    /// <typeparam name="TConsumer">Тип потребителя сообщений.</typeparam>
    /// <param name="handleMessage">Обработчик сообщения.</param>
    public IBusProvider Subscribe<TMessage, TConsumer>(Func<TMessage, IMessageContext, Task> handleMessage)
    {
        repository.Add<TMessage, TConsumer>(handleMessage);
        return this;
    }
}
