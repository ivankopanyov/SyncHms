namespace SyncHms.Identity.Sqlite;

public class SqliteIdentityOptions
{
    public const string Section = "SqliteIdentity";
    
    public string ConnectionString { get; set; } = "Data Source=identity.db";
    
    public int AccessTokenExpirationMinutes { get; set; } = 1;

    public int RefreshTokenExpirationDays { get; set; } = 7;

    public bool UseMigrations { get; set; } = true;

    public Microsoft.AspNetCore.Identity.IdentityOptions Options { get; internal set; }
}
