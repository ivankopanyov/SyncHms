namespace SyncHms.Identity.Repositories.Implement;

internal class ConnectionRepository(ICache cache) : IConnectionRepository
{
    public async Task PushAsync(string id, long userId, TimeSpan expiration)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));
        await cache.PushAsync(GetKey(id, userId), new Connection());
    }

    public async Task<bool> RemoveAsync(string id, long userId)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));
        return await cache.PopAsync<Connection>(GetKey(id, userId)) != null;
    }

    private static string GetKey(string id, long userId) => $"{userId}_{id}";
}
