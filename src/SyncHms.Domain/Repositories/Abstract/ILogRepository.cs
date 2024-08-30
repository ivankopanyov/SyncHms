namespace SyncHms.Domain;

/// <summary>Интерфейс, определяющий методы репозитория для работы с логами.</summary>
public interface ILogRepository
{
    /// <summary>Метод, сохраняющий лог.</summary>
    /// <param name="log">Лог для сохранения.</param>
    Task AddAsync(Log log);

    /// <summary>Метод, возвращающий логи, относящиес к задаче с указанным идентификатором.</summary>
    /// <param name="taskId">Идентификатор задачи.</param>
    /// <returns>Если не найдено логов, относящихся к указанной задаче, вернет <c>null</c></returns>
    Task<IEnumerable<Log>?> GetAsync(string taskId);

    /// <summary>Метод, возвращающий данные лога с указанным идентификатором.</summary>
    /// <param name="logId">Идентификатор лога.</param>
    /// <returns>Данные указанного лога.</returns>
    Task<LogData?> GetDataAsync(string logId);

    /// <summary>Метод, осуществляющий поиск по логам.</summary>
    /// <param name="filter">Фильтр поиска.</param>
    /// <returns>
    /// Найденные логи, удовлетворяющие условиям фильтра.
    /// Если значение фильтра <c>null</c>, будут возвращены все логи.
    /// </returns>
    Task<IEnumerable<Log>> FindAsync(SearchFilter? filter = null);
}