namespace SyncHms.Identity;

/// <summary>Класс, описывающий модель опций сервисов идентификации пользователей.</summary>
public class IdentityOptions
{
    /// <summary>Время жизни токена доступа в минутах.</summary>
    public int AccessTokenExpirationMinutes { get; set; }

    /// <summary>Время жизни токена обновления токена доступа в днях.</summary>
    public int RefreshTokenExpirationDays { get; set; }
    
    /// <summary>
    /// Свойство определяет, будут ли использоваться миграции при работе с базой данных.<br/>
    /// По умолчанию <c>true</c>.
    /// </summary>
    public bool UseMigrations { get; set; }
    
    /// <summary>Инициализация опций системы идентификации пользователей.</summary>
    public Action<Microsoft.AspNetCore.Identity.IdentityOptions>? SetupAction { get; set; }
}