namespace SyncHms.Identity;

public interface IIdentityService
{
    Task<bool> ExistsAsync();

    Task<Token> SignUpAsync(string username, string password);

    Task<Token?> SignInAsync(string username, string password);

    Task<Token?> RefreshAsync(long userId, string refreshToken);
    
    Task SignOutAsync(long userId, string refreshToken);
}
