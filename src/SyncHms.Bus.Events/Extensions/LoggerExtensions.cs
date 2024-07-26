namespace SyncHms.Bus.Events.Extensions;

internal static class LoggerExtensions
{
    public const string Queue = "QueueName";

    public const string Handler = "HandlerName";

    public const string Task = "TaskId";

    public static void LogEvent(this ILogger logger, EventLog eventLog, Exception? ex = null)
    {
        var state = new Dictionary<string, object>()
        {
            { Queue, FixLog(eventLog.TaskName, "UNKNOWN_QUEUE") },
            { Handler, FixLog(eventLog.HandlerName, "UNKNOWN_HANDLER") },
            { Task, FixLog(eventLog.TaskId, "\"UNKNOWN_TASK\"") }
        };

        var logLevel = LogLevel.Information;
        if (eventLog.IsError)
            logLevel = eventLog.IsEnd ? LogLevel.Critical : LogLevel.Error;

        using (logger.BeginScope(state))
        {
            logger.Log(logLevel, ex, FixLog(eventLog.Message, "No message."));
        };
    }

    private static string FixLog(string? value, string defaultLog)
    {
        value = value?.Trim();
        return string.IsNullOrEmpty(value) ? defaultLog : value;
    }
}
