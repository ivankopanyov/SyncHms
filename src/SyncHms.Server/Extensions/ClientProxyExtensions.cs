namespace SyncHms.Server.Extensions;

/// <summary>
/// Статический класс, который содержит методы расширения для интерфейса <see cref="IClientProxy"/>.
/// </summary>
public static class ClientProxyExtensions
{
    /// <summary>Время задержки в миллисекундах перед повторной попыткой запроса в случае неудачи.</summary>
    private const int MillisecondsDelay = 1000;

    /// <summary>
    /// Вызывает метод для соединений, представленных экземпляром <see cref="IClientProxy"/>.
    /// Не ждет ответа от получателя.<br/>
    /// Предпринимает несколько попыток, в случае неудачи не возбуждает исключение.
    /// </summary>
    /// <param name="method">Метод вызываемого события.</param>
    /// <param name="arg1">Аргумент 1.</param>
    /// <param name="logger">Объект логгера.</param>
    public static async Task TrySendAsync(this IClientProxy clientProxy, string method, object? arg1, ILogger logger)
    {
        for (var i = 1; i <= 3; i++)
        {
            try
            {
                await clientProxy.SendAsync(method, arg1);
                return;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Try #{i} {ex.Message}");
                await Task.Delay(MillisecondsDelay);
            }
        }
    }

    /// <summary>
    /// Вызывает метод для соединений, представленных экземпляром <see cref="IClientProxy"/>.
    /// Не ждет ответа от получателя.<br/>
    /// Предпринимает несколько попыток, в случае неудачи не возбуждает исключение.
    /// </summary>
    /// <param name="method">Метод вызываемого события.</param>
    /// <param name="arg1">Аргумент 1.</param>
    /// <param name="arg2">Аргумент 2.</param>
    /// <param name="logger">Объект логгера.</param>
    public static async Task TrySendAsync(this IClientProxy clientProxy, string method, object? arg1, object? arg2, ILogger logger)
    {
        for (var i = 1; i <= 3; i++)
        {
            try
            {
                await clientProxy.SendAsync(method, arg1, arg1);
                return;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Try #{i} {ex.Message}");
                await Task.Delay(MillisecondsDelay);
            }
        }
    }
}