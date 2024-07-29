namespace SyncHms.Identity.Sqlite.Options;

public class SqliteIdentityOptions : IdentityOptions
{
    public string ConnectionString { get; set; } = "Data Source=identity.db";
}
