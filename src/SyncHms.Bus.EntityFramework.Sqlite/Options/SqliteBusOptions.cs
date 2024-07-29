namespace SyncHms.Bus.EntityFramework.Sqlite.Options;

public class SqliteBusOptions : EntityFrameworkBusOptions
{
    public string ConnectionString { get; set; } = "Data Source=bus.db";
}
