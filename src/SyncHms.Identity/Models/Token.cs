namespace SyncHms.Identity;

/// <summary>Класс, описывающий модель токена для авторизации пользователя.</summary>
public class Token
{
    /// <summary>Токен доступа.</summary>
    public string AccessToken { get; set; }
    
    /// <summary>Токен для обновления токена доступа.</summary>
    public string RefreshToken { get; set; }
    
    /// <summary>Время жизни токена.</summary>
    public TimeSpan Expiration { get; set; }
}