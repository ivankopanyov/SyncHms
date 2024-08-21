namespace SyncHms.Identity.Services.Implement;

/// <summary>
/// Класс, описывающий сервис, генерирующий токены доступа.<br/>
/// Реализует интерфейс <see cref="ITokenService"/>
/// </summary>
/// <param name="options">Экземпляр опций генерации токенов доступа.</param>
/// <param name="identityOptions">Экземпляр опций сервиса идентификации пользователей.</param>
internal class JwtService(IOptions<JwtBearerOptions> options, IdentityOptions identityOptions) : ITokenService
{
    /// <summary>Время жизни токена обновения токена доступа.</summary>
    private readonly TimeSpan _expiration = TimeSpan.FromDays(identityOptions.RefreshTokenExpirationDays);
    
    /// <summary>Метод, генерирующий токены доступа.</summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Токен доступа.</returns>
    public Token Generate(long userId) => new()
    {
        AccessToken = AccessToken(userId),
        RefreshToken = RefreshToken(),
        Expiration = _expiration
    };
    
    /// <summary>Метод, генерирующий токен доступа.</summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Токен доступа.</returns>
    private string AccessToken(long userId)
    {
        var claims = new Claim[] { new(ClaimTypes.NameIdentifier, userId.ToString()) };
        var signingCredentials = new SigningCredentials(options.Value.TokenValidationParameters.IssuerSigningKey, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(identityOptions.AccessTokenExpirationMinutes);
        var token = new JwtSecurityToken(claims: claims, expires: expiration, signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

    /// <summary>Метод, генерирующий токен обновления токена доступа.</summary>
    /// <returns>Токен обновления токена доступа.</returns>
    private static string RefreshToken()
    {
        var randomNumber = new byte[32];
        Random.Shared.NextBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
