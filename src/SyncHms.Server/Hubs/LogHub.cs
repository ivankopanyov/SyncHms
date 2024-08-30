namespace SyncHms.Server.Hubs;

/// <summary>
/// Класс, описывающий концентратор логов <c>SignalR</c><br/>
/// Унаследован от класса <see cref="Hub"/>
/// </summary>
/// <param name="logRepository">Экземпляр репозитория для работы с логами.</param>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class LogHub(ILogRepository logRepository) : Hub
{
    /// <summary>Конечная точка концентратора для запроса логов.</summary>
    /// <param name="size">Максимальное колличество возвращаемых логов.</param>
    public async Task Tasks(int size)
    {
        await SendAsync(new SearchFilter
        {
            Size = size
        });
    }
    
    /// <summary>Конечная точка концентратора для запроса логов.</summary>
    /// <param name="from">Минимальная дата создания логов, от которой начинается поиск.</param>
    public async Task Update(DateTime from)
    {
        await SendAsync(new SearchFilter
        {
            From = from
        }, true);
    }

    /// <summary>Метод, осуществляющий поиск логов и возвращающий результат клиенту.</summary>
    /// <param name="filter">Экземпляр фильтра поиска логов.</param>
    /// <param name="update">Флаг, указывающий, нужно ли обновить список логов в клиентском приложении.</param>
    private async Task SendAsync(SearchFilter filter, bool update = false)
    {
        var logs = await logRepository.FindAsync(filter);
        await Clients.Client(Context.ConnectionId).SendAsync("Logs", logs, update);
    }
}
