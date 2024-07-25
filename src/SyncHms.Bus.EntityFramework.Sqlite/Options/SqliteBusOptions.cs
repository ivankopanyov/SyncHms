namespace SyncHms.Bus;

public class SqliteBusOptions
{
    public string ConnectionString { get; set; } = "Data Source=bus.db";

    public InitDatabase InitDatabase { get; set; } = InitDatabase.Migrate;
}
