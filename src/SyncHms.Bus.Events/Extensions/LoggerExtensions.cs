namespace SyncHms.Bus.Events.Extensions;

/// <summary>Статический класс, содеращий методы расширения интерфейса <see cref="ILogger"/></summary>
internal static class LoggerExtensions
{
    /// <summary>Значение ключа для аргумента, хранящего имя очереди.</summary>
    public const string Queue = "QueueName";

    /// <summary>Значение ключа для аргумента, хранящего имя обработчика.</summary>
    public const string Handler = "HandlerName";

    /// <summary>Значение ключа для аргумента, хранящего идентификатор задачи.</summary>
    public const string Task = "TaskId";

    /// <summary>Метод логирования события.</summary>
    /// <param name="eventLog">Экземпляр лога события.</param>
    /// <param name="ex">Исключение, возбужденное в процессе обработки события.</param>
    public static void LogEvent(this ILogger logger, EventLog eventLog, Exception? ex = null)
    {
        var state = new Dictionary<string, object>
        {
            { Queue, FixLog(eventLog.TaskName, "UNKNOWN_QUEUE") },
            { Handler, FixLog(eventLog.HandlerName, "UNKNOWN_HANDLER") },
            { Task, FixLog(eventLog.TaskId, "\"UNKNOWN_TASK\"") }
        };

        var logLevel = LogLevel.Information;
        if (eventLog.Error != null)
            logLevel = eventLog.IsEnd ? LogLevel.Critical : LogLevel.Error;

        using (logger.BeginScope(state))
        {
            logger.Log(logLevel, ex, FixLog(eventLog.Message, "No message."));
        };
    }

    /// <summary>Метод, заменяющий пустое значение аргумента на значение по умолчанию.</summary>
    /// <param name="value">Текущее значение.</param>
    /// <param name="defaultLog">Значение по умолчанию.</param>
    /// <returns>Итоговое значение.</returns>
    private static string FixLog(string? value, string defaultLog)
    {
        value = value?.Trim();
        return string.IsNullOrEmpty(value) ? defaultLog : value;
    }
}
