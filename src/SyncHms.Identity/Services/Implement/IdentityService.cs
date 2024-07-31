namespace SyncHms.Identity.Services.Implement;

internal class IdentityService(IIdentityContextFactory contextFactory, UserManager<User> userManager,
    ITokenService tokenService, IConnectionRepository connectionRepository) : IIdentityService
{
    private static readonly SemaphoreSlim _semaphore = new(1);
    
    public async Task<bool> AnyAsync()
    {
        await _semaphore.WaitAsync();

        try
        {
            return await userManager.Users.AsNoTracking().AnyAsync();
        }
        finally
        {
            _semaphore.Release();
        }
    }
    
    public async Task<Token> SignUpAsync(string username, string password)
    {
        await _semaphore.WaitAsync();

        try
        {
            if (await userManager.Users.AsNoTracking().AnyAsync())
                throw new MethodAccessException();

            var user = new User
            {
                UserName = username,
                CanModified = false
            };

            var result = await userManager.CreateAsync(user, password);

            return result.Succeeded
                ? await RefreshTokenAsync(user.Id)
                : throw new InvalidOperationException(string.Join(' ', result.Errors.Select(e => e.Description)));
        }
        finally
        {
            _semaphore.Release();
        }
    }
    
    public async Task<Token?> SignInAsync(string username, string password)
    {
        if (await userManager.FindByNameAsync(username.Trim().ToLower()) is not { } user)
        {
            return !await userManager.Users.AsNoTracking().AnyAsync()
                ? throw new MethodAccessException()
                : null;
        }

        return await userManager.CheckPasswordAsync(user, password)
            ? await RefreshTokenAsync(user.Id)
            : null;
    }
    
    public async Task<Token?> RefreshAsync(long userId, string refreshToken)
    {
        return await connectionRepository.RemoveAsync(refreshToken, userId)
            ? await RefreshTokenAsync(userId)
            : null;
    }
    
    public async Task SignOutAsync(long userId, string refreshToken) =>
        await connectionRepository.RemoveAsync(refreshToken, userId);

    private async Task<Token> RefreshTokenAsync(long userId)
    {
        var token = tokenService.Generate(userId);
        await connectionRepository.PushAsync(token.RefreshToken, userId, token.Expiration);
        return token;
    }
}
