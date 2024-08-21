namespace SyncHms.Server.Hubs;


/// <summary>
/// Класс, описывающий концентратор сервисов <c>SignalR</c><br/>
/// Унаследован от класса <see cref="Hub"/>
/// </summary>
/// <param name="serviceRepository">Экземпляр репозитория для работы с сервисами.</param>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ServiceHub(IServiceRepository serviceRepository) : Hub
{
    /// <summary>Конечная точка концентратора для запроса состояния сервисов.</summary>
    public async Task Services()
    {
        await Clients
            .Client(Context.ConnectionId)
            .SendAsync("Services", await serviceRepository.GetAllAsync());
    }
}
