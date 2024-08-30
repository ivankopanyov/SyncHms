namespace SyncHms.Bus.EntityFramework.Repositories.Abstract;

/// <summary>Интерфейс, определяющий методы для работы с репозиторием хранилищ сообщений шины данных.</summary>
internal interface IExchangeRepository
{
    /// <summary>
    /// Метод добавляет обработчик для определённого типа сообщения
    /// и типа потребителя, если он еще не был добавлен.
    /// </summary>
    /// <typeparam name="TMessage">Тип сообщения.</typeparam>
    /// <typeparam name="TConsumer">Тип потребителя.</typeparam>
    /// <param name="handleMessage">Функция, которая будет вызываться при получении сообщения.</param>
    void Add<TMessage, TConsumer>(Func<TMessage, IMessageContext, Task> handleMessage);

    /// <summary>Метод добавляет сообщение в очередь.</summary>
    /// <typeparam name="TMessage">Тип сообщения</typeparam>
    /// <param name="message">Экземпляр сообщения.</param>
    Task AddMessageAsync<TMessage>(TMessage message);
}
