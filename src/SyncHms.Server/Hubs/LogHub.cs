namespace SyncHms.Server.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class LogHub(ILogRepository logRepository) : Hub
{
    public async Task Tasks(int size)
    {
        await SendAsync(new SearchFilter
        {
            Size = size
        });
    }

    public async Task Update(DateTime from)
    {
        await SendAsync(new SearchFilter
        {
            From = from
        }, true);
    }

    private async Task SendAsync(SearchFilter filter, bool update = false)
    {
        var logs = await logRepository.FindAsync(filter);
        await Clients.Client(Context.ConnectionId).SendAsync("Logs", logs, update);
    }
}
