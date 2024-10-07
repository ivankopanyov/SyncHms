namespace SyncHms.Domain.EntityFramework.Repositories.Implement;

/// <summary>
/// Класс, описывающий репозиторий для работы с логами.<br/>
/// Реализует интерфейс <see cref="ILogRepository"/>
/// </summary>
/// <param name="domainContextFactory">
/// Экземпляр фабрики, создающей контекст подключения к базе данных домена.
/// </param>
internal class LogRepository(IDomainContextFactory domainContextFactory) : ILogRepository
{
    /// <summary>Метод, сохраняющий лог.</summary>
    /// <param name="log">Лог для сохранения.</param>
    public async Task AddAsync(Log log)
    {
        await using var context = domainContextFactory.Create();
        var logData = log.LogData;
        log.LogData = null;
        await context.Logs.AddAsync(log);
        await context.SaveChangesAsync();
        
        if (logData != null)
        {
            log.LogDataId = logData.Id;
            logData.LogId = log.Id;
            await context.LogDatas.AddAsync(logData);
            await context.SaveChangesAsync();
        }
    }
    
    /// <summary>Метод, возвращающий логи, относящиес к задаче с указанным идентификатором.</summary>
    /// <param name="taskId">Идентификатор задачи.</param>
    /// <returns>Если не найдено логов, относящихся к указанной задаче, вернет <c>null</c></returns>
    public async Task<IEnumerable<Log>?> GetAsync(string taskId)
    {
        await using var context = domainContextFactory.Create();
        var logs = await context.Logs
            .AsNoTracking()
            .Where(l => l.TaskId == taskId)
            .ToListAsync();

        return logs.Count == 0 ? null : logs;
    }

    /// <summary>Метод, возвращающий данные лога с указанным идентификатором.</summary>
    /// <param name="logId">Идентификатор лога.</param>
    /// <returns>Данные указанного лога.</returns>
    public async Task<LogData?> GetDataAsync(string logId)
    {
        await using var context = domainContextFactory.Create();
        return await context.LogDatas
            .AsNoTracking()
            .FirstOrDefaultAsync(ld => ld.LogId == logId);
    }

    /// <summary>Метод, осуществляющий поиск по логам.</summary>
    /// <param name="filter">Фильтр поиска.</param>
    /// <returns>
    /// Найденные логи, удовлетворяющие условиям фильтра.
    /// Если значение фильтра <c>null</c>, будут возвращены все логи.
    /// </returns>
    public async Task<IEnumerable<Log>> FindAsync(SearchFilter? filter = null)
    {
        if (filter?.Size is <= 0)
            return [];

        var raw = SelectRaw(filter?.Pattern);
        await using var context = domainContextFactory.Create();
        var query = context.Logs.FromSqlRaw(string.Format(raw));

        if (filter != null)
        {
            if (filter.IsError is { } isError)
                query = query.Where(l => l.IsError == isError);

            if (filter.IsEnd is { } isEnd)
                query = query.Where(l => l.IsEnd == isEnd);

            if (filter.TaskNames?.Count is > 0)
                query = query.Where(l => l.TaskName != null &&
                                         filter.TaskNames.Contains(l.TaskName!));

            if (filter.To is { } to)
                query = query.Where(l => l.DateTime < to);
            
            if (filter.From is { } from)
                query = query.Where(l => l.DateTime > from);

            if (filter.Size is { } size)
                query = query.Take(size);
        }

        return await query.ToListAsync();
    }

    /// <summary>Метод, формирующий запрос поиска по логам.</summary>
    /// <param name="pattern">Шаблон поиска.</param>
    /// <returns>Запрос поиска по логам.</returns>
    private static string SelectRaw(string? pattern)
    {
        var isPattern = !string.IsNullOrWhiteSpace(pattern);

        var stringBuilder = new StringBuilder()
            .Append("SELECT l.Id, l.LogDataId, l.TaskId, l.TaskName, l.HandlerName, l.DateTime, l.IsError, l.IsEnd, l.Message");

        if (isPattern)
            stringBuilder
                .Append(", (SELECT GROUP_CONCAT(InputObjectJson) AS InputObjectJson FROM LogDatas as ld WHERE l.TaskId = ld.TaskId) AS InputObjectJson");

        stringBuilder
            .Append(" FROM (SELECT Id, LogDataId, TaskId, TaskName, HandlerName, MAX(DateTime) as DateTime, IsError, IsEnd, Message FROM Logs GROUP BY TaskId ORDER BY DateTime DESC) AS l");

        if (isPattern)
            stringBuilder
                .Append(" WHERE InputObjectJson LIKE '%")
                .Append(pattern)
                .Append("%'");

        return stringBuilder.ToString();
    }
}
