namespace SyncHms.Server.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class EnvironmentHub(IEnvironmentRepository<ApplicationEnvironment> environmentRepository) : Hub
{
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
