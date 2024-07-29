namespace SyncHms.Identity.Infrastructure;

public class UserIdProvider: IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection) =>
        connection.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
}
