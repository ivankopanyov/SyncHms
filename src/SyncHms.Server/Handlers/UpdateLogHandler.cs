namespace SyncHms.Server.Handlers;

/// <summary>
/// Класс, описывающий обработчик логов обработки событий.<br/>
/// Унаследован от класса <see cref="LogHandler"/>
/// </summary>
/// <param name="logRepository">Экземпляр репозитория для работы с логами.</param>
/// <param name="hubContext">Контекст концентратора логов <c>SignalR</c></param>
public class UpdateLogHandler(ILogRepository logRepository, IHubContext<LogHub> hubContext) : LogHandler
{
    /// <summary>
    /// Метод, обрабатывающий лог обработки события.<br/>
    /// Отправляет информацию о событии клиентам приложения с помощью <c>SignalR</c>
    /// </summary>
    /// <param name="in">Экземпляр лога обработки события.</param>
    protected override async Task HandleAsync(EventLog @in)
    {
        if (@in.Service)
            return;

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
                TaskId = @in.TaskId,
                Error = @in.Error,
                StackTrace = @in.StackTrace,
                InputObjectJson = @in.InputObjectJson
            }
        };

        await logRepository.AddAsync(log);
        await hubContext.Clients.All.SendAsync("Log", log);
    }
}
