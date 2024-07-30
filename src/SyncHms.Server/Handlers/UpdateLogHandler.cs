namespace SyncHms.Server.Handlers;

public class UpdateLogHandler(ILogRepository logRepository, IHubContext<LogHub> hubContext) : LogHandler
{
    protected override async Task HandleAsync(EventLog @in)
    {
        var log = new Log
        {
            TaskId = @in.TaskId,
            TaskName = @in.TaskName,
            HandlerName = @in.HandlerName,
            DateTime = @in.DateTime,
            IsError = @in.Error != null,
            IsEnd = @in.IsEnd,
            Message = @in.Message,
            LogData = new LogData
            {
                Error = @in.Error,
                StackTrace = @in.StackTrace,
                InputObjectJson = @in.InputObjectJson
            }
        };

        await logRepository.AddAsync(log);

        log.LogData = null;
        await hubContext.Clients.All.SendAsync("Log", log);
    }
}
