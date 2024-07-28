namespace SyncHms.Domain;

public interface ILogRepository
{
    Task AddAsync(Log log);

    Task<IEnumerable<Log>?> GetAsync(string taskId);

    Task<LogData?> GetDataAsync(string logId);

    Task<IEnumerable<Log>> FindAsync(SearchFilter? filter = null);
}