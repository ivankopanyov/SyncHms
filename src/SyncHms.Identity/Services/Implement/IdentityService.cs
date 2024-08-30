namespace SyncHms.Identity.Services.Implement;

/// <summary>
/// Класс, описывающий сервис идентификации пользователей.<br/>
/// Реализует интерфейс <see cref="IIdentityService"/>
/// </summary>
/// <param name="userManager">Экземпляр менеджера пользователей</param>
/// <param name="tokenService">Экземпляр сервиса генерации токенов доступа.</param>
/// <param name="connectionRepository">Экземпляр репзитория подключений пользователей.</param>
internal class IdentityService(UserManager<User> userManager, ITokenService tokenService,
    IConnectionRepository connectionRepository) : IIdentityService
{
    private static readonly SemaphoreSlim Semaphore = new(1);
    
    /// <summary>Метод, проверяющий наличие зарегистрированных пользователей.</summary>
    /// <returns>Флаг, указывающий, был ли зарегистрирован хотя бы один пользователь.</returns>
    public async Task<bool> AnyAsync()
    {
        await Semaphore.WaitAsync();

        try
        {
            return await userManager.Users.AsNoTracking().AnyAsync();
        }
        finally
        {
            Semaphore.Release();
        }
    }
    
    /// <summary>Метод регистрации пользователей.</summary>
    /// <param name="username">Имя пользователя.</param>
    /// <param name="password">Пароль пользователя.</param>
    /// <returns>Токен для авторизации пользователя.</returns>
    /// <exception cref="MethodAccessException">
    /// Исключение, возбуждаемое, если первый пользователь уже был зарегистрирован.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Исключение, возбуждаемое, если не удалось зарегистрировать пользователя.
    /// </exception>
    public async Task<Token> SignUpAsync(string username, string password)
    {
        await Semaphore.WaitAsync();

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
            Semaphore.Release();
        }
    }
    
    /// <summary>Метод аутентификации пользователей.</summary>
    /// <param name="username">Имя пользователя.</param>
    /// <param name="password">Пароль пользователя.</param>
    /// <returns>
    /// Токен для авторизации пользователя.<br/>
    /// Вернет <c>null</c> - если пользователь с указанными данными не зарегистрирован.
    /// </returns>
    /// <exception cref="MethodAccessException">
    /// Исключение, возбуждаемое, если первый пользователь еще не был зарегистрирован.
    /// </exception>
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
    
    /// <summary>Метод обновления токена доступа пользователя.</summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="refreshToken">Токен обновления токена доступа.</param>
    /// <returns>
    /// Токен для авторизации пользователя.<br/>
    /// Вернет <c>null</c> - если пользователь с указанными данными не найден.
    /// </returns>
    public async Task<Token?> RefreshAsync(long userId, string refreshToken)
    {
        return await connectionRepository.RemoveAsync(refreshToken, userId)
            ? await RefreshTokenAsync(userId)
            : null;
    }
    
    /// <summary>Метод выхода пользователя.</summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="refreshToken">Токен обновления токена доступа.</param>
    public async Task SignOutAsync(long userId, string refreshToken) =>
        await connectionRepository.RemoveAsync(refreshToken, userId);

    /// <summary>Метод, генерирующий токен доступа.</summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Токен для авторизации пользователя.</returns>
    private async Task<Token> RefreshTokenAsync(long userId)
    {
        var token = tokenService.Generate(userId);
        await connectionRepository.PushAsync(token.RefreshToken, userId, token.Expiration);
        return token;
    }
}
