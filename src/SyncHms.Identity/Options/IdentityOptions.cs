namespace SyncHms.Identity;

public class IdentityOptions
{
    public int AccessTokenExpirationMinutes { get; set; }

    public int RefreshTokenExpirationDays { get; set; }

    public bool UseMigrations { get; set; }
    
    public Action<Microsoft.AspNetCore.Identity.IdentityOptions>? SetupAction { get; set; }
}