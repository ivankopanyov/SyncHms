namespace SyncHms.Identity;

public class SqliteOptions : DatabaseOptions
{
    public int AccessTokenExpirationMinutes { get; set; } = 1;

    public int RefreshTokenExpirationDays { get; set; } = 7;
}
