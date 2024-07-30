namespace SyncHms.Server.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ServiceHub(IServiceRepository serviceRepository) : Hub
{
    public async Task Services()
    {
        await Clients
            .Client(Context.ConnectionId)
            .SendAsync("Services", await serviceRepository.GetAllAsync());
    }
}
