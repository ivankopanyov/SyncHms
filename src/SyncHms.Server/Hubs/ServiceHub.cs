namespace SyncHms.Server.Hubs;


/// <summary>
/// Класс, описывающий концентратор сервисов <c>SignalR</c><br/>
/// Унаследован от класса <see cref="Hub"/>
/// </summary>
/// <param name="serviceRepository">Экземпляр репозитория для работы с сервисами.</param>
/// <param name="logger">Экземпляр логгера.</param>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ServiceHub(IServiceRepository serviceRepository, ILogger<ServiceHub> logger) : Hub
{
    /// <summary>Конечная точка концентратора для запроса состояния сервисов.</summary>
    public async Task Services()
    {
        await Clients
            .Client(Context.ConnectionId)
            .TrySendAsync("Services", await serviceRepository.GetAllAsync(), logger);
    }
}
