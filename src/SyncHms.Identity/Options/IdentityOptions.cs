namespace SyncHms.Identity;

public class IdentityOptions
{
    public int AccessTokenExpirationMinutes { get; set; } = 1;

    public int RefreshTokenExpirationDays { get; set; } = 7;

    public bool UseMigrations { get; set; } = true;

    public Microsoft.AspNetCore.Identity.IdentityOptions Options { get; internal set; }
}