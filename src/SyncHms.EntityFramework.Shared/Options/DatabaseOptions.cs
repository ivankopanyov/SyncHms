namespace SyncHms.EntityFramework.Shared;

public class DatabaseOptions
{
    public string ConnectionString { get; set; }

    public InitDatabase InitDatabase { get; set; } = InitDatabase.Migrate;
}
