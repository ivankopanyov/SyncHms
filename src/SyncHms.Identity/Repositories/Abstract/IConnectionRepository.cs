namespace SyncHms.Identity.Repositories.Abstract;

internal interface IConnectionRepository
{
    Task PushAsync(string id, long userId, TimeSpan expiration);

    Task<bool> RemoveAsync(string id, long userId);
}
