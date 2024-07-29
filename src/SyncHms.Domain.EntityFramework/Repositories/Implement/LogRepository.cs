namespace SyncHms.Domain.EntityFramework.Repositories.Implement;

internal class LogRepository(IDomainContextFactory domainContextFactory) : ILogRepository
{
    public async Task AddAsync(Log log)
    {
        await using var context = domainContextFactory.Create();
        await context.Logs.AddAsync(log);
        await context.SaveChangesAsync();
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
        IQueryable<IGrouping<string, Log>> query = context.Logs
            .AsNoTracking()
            .OrderByDescending(l => l.DateTime)
            .GroupBy(l => l.TaskId)
            .OrderByDescending(g => g.First().DateTime);

        if (filter != null)
        {
            if (filter.IsError is { } isError)
                query = query.Where(g => g.First().IsError == isError);

            if (filter.IsEnd is { } isEnd)
                query = query.Where(g => g.First().IsEnd == isEnd);

            if (filter.TaskNames?.Count is > 0)
                query = query.Where(g => g.First().TaskName != null && filter.TaskNames.Contains(g.First().TaskName));
            
            // pattern

            if (filter.To is { } to)
                query = query.Where(g => g.First().DateTime < to);
            
            if (filter.To is { } from)
                query = query.Where(g => g.First().DateTime > from);

            if (filter.Size is { } size)
                query = query.Take(size);
        }

        return await query.SelectMany(g => g).ToListAsync();
    }
}
