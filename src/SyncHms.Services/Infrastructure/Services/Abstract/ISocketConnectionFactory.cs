namespace SyncHms.Services.Infrastructure.Services.Abstract;

/// <summary>
/// Интерфейс, определяющий методы фабрики, создающий объекты для подключения к сокету.
/// <seealso cref="ISocketConnection"/>
/// </summary>
internal interface ISocketConnectionFactory
{
    /// <summary>Метод, создающий объект для подключения к сокету.</summary>
    /// <param name="host">Имя или адрес хоста для подключения.</param>
    /// <param name="port">Номер порта для подключения.</param>
    /// <param name="head">Символ, определяющий начало сообщения.</param>
    /// <param name="tail">Символ, определяющий конец сообщения.</param>
    /// <param name="connectionDelay">
    /// Время задержки в секундах перед попыткой усановления соединения с сокетом.
    /// </param>
    /// <param name="connectionTimeout">
    /// Максимальное время в секундах для попытки усановления соединения с сокетом.
    /// </param>
    /// <returns>Экземпляр объекта для подключения к сокету.</returns>
    ISocketConnection Create(string host, int port, char head, char tail,
        int connectionDelay = 6, int connectionTimeout = 1);
}
