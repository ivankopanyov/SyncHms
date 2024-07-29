namespace SyncHms.Identity.Services.Implement;

internal class JwtService(IOptions<JwtBearerOptions> options, IdentityOptions identityOptions) : ITokenService
{
    private readonly TimeSpan _expiration = TimeSpan.FromDays(identityOptions.RefreshTokenExpirationDays);
    
    public Token Generate(long userId) => new()
    {
        AccessToken = AccessToken(userId),
        RefreshToken = RefreshToken(),
        Expiration = _expiration
    };
    
    private string AccessToken(long userId)
    {
        var claims = new Claim[] { new(ClaimTypes.NameIdentifier, userId.ToString()) };
        var signingCredentials = new SigningCredentials(options.Value.TokenValidationParameters.IssuerSigningKey, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(identityOptions.AccessTokenExpirationMinutes);
        var token = new JwtSecurityToken(claims: claims, expires: expiration, signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

    private static string RefreshToken()
    {
        var randomNumber = new byte[32];
        Random.Shared.NextBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
