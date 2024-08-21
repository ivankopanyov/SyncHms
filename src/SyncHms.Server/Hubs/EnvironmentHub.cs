namespace SyncHms.Server.Hubs;

/// <summary>
/// Класс, описывающий концентратор окружения <c>SignalR</c><br/>
/// Унаследован от класса <see cref="Hub"/>
/// </summary>
/// <param name="environmentRepository">Экземпляр репозитория для работы с окружением.</param>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class EnvironmentHub(IEnvironmentRepository<ApplicationEnvironment> environmentRepository) : Hub
{
    /// <summary>Конечная точка концентратора для запроса экземпляра текущего окружения.</summary>
    public async Task Environment()
    {
        await Clients
            .Client(Context.ConnectionId)
            .SendAsync("Environment", JsonConvert.SerializeObject(await environmentRepository.GetAsync() ?? new(), new JsonSerializerSettings()
            {
                ContractResolver = new DescriptionContractResolver()
            }));
    }
}
