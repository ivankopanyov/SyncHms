namespace SyncHms.Domain.EntityFramework.Repositories.Implement;

internal class LogRepository(IDomainContextFactory domainContextFactory) : ILogRepository
{
    private const string SelectRaw = "SELECT * FROM (" +
        "SELECT Id, LogDataId, TaskId, TaskName, HandlerName, MAX(DateTime) as DateTime, IsError, IsEnd, Message " +
        "FROM Logs GROUP BY TaskId ORDER BY DateTime DESC)";
    
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

    public async Task<IEnumerable<Log>?> GetAsync(string taskId)
    {
        await using var context = domainContextFactory.Create();
        var logs = await context.Logs
            .AsNoTracking()
            .Where(l => l.TaskId == taskId)
            .ToListAsync();

        return logs.Count == 0 ? null : logs;
    }

    public async Task<LogData?> GetDataAsync(string logId)
    {
        await using var context = domainContextFactory.Create();
        return await context.LogDatas
            .AsNoTracking()
            .FirstOrDefaultAsync(ld => ld.LogId == logId);
    }

    public async Task<IEnumerable<Log>> FindAsync(SearchFilter? filter = null)
    {
        if (filter?.Size is <= 0)
            return [];

        await using var context = domainContextFactory.Create();
        var query = context.Logs.FromSqlRaw(string.Format(SelectRaw));

        if (filter != null)
        {
            if (filter.IsError is { } isError)
                query = query.Where(l => l.IsError == isError);

            if (filter.IsEnd is { } isEnd)
                query = query.Where(l => l.IsEnd == isEnd);

            if (filter.TaskNames?.Count is > 0)
                query = query.Where(l => l.TaskName != null &&
                                         filter.TaskNames.Contains(l.TaskName!));
            
            // pattern

            if (filter.To is { } to)
                query = query.Where(l => l.DateTime < to);
            
            if (filter.From is { } from)
                query = query.Where(l => l.DateTime > from);

            if (filter.Size is { } size)
                query = query.Take(size);
        }

        return await query.ToListAsync();
    }
}
