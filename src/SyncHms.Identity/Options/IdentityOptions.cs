namespace SyncHms.Identity;

public class IdentityOptions : EntityFrameworkOptions
{
    public int AccessTokenExpirationMinutes { get; set; }

    public int RefreshTokenExpirationDays { get; set; }
}