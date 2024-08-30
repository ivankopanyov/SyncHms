namespace SyncHms.Bus.Services.Extensions;

/// <summary>Статический класс, содеращий методы расширения интерфейса <see cref="ILogger"/></summary>
internal static class LoggerExtensions
{
    /// <summary>Значение ключа для аргумента, хранящего имя сервиса.</summary>
    public const string Service = "ServiceName";

    /// <summary>Метод, логирующий удачное подключение сервиса к удаленному ресурсу,</summary>
    /// <param name="serviceName">Имя сервиса.</param>
    public static void LogActive(this ILogger logger, string serviceName)
    {
        var state = new Dictionary<string, object>()
        {
            { Service, serviceName ?? string.Empty }
        };

        using (logger.BeginScope(state))
        {
            logger.LogInformation("Active.");
        };
    }

    /// <summary>Метод, логирующий неудачную попытку подключения сервиса к удаленному ресурсу,</summary>
    /// <param name="serviceName">Имя сервиса.</param>
    /// <param name="message">Сообщение об ошибке подключение.</param>
    /// <param name="ex">Исключение, возбужденное в результате попытки подключения.</param>
    public static void LogUnactive(this ILogger logger, string serviceName, string? message = null, Exception? ex = null)
    {
        var state = new Dictionary<string, object>()
        {
            { Service, serviceName ?? string.Empty }
        };

        using (logger.BeginScope(state))
        {
            logger.LogError(ex, $"Unactive: {message ?? ex?.Message ?? "No message."}");
        };
    }

    /// <summary>Метод, логирующий предупреждение, возникшее в процессе подключения.</summary>
    /// <param name="serviceName">Имя сервиса.</param>
    /// <param name="message">Сообщение об ошибке подключение.</param>
    /// <param name="ex">Исключение, возбужденное в результате подключения.</param>
    public static void LogWarning(this ILogger logger, string serviceName, string? message = null, Exception? ex = null)
    {
        var state = new Dictionary<string, object>()
        {
            { Service, serviceName ?? string.Empty }
        };

        using (logger.BeginScope(state))
        {
            logger.LogWarning(ex, $"Warning: {message ?? ex?.Message ?? "No message."}");
        };
    }
}
