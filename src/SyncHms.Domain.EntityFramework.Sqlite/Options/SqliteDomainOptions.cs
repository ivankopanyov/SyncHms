namespace SyncHms.Domain.EntityFramework.Sqlite.Options;

public class SqliteDomainOptions : EntityFrameworkDomainOptions
{
    public string ConnectionString = "Data Source=app.db";
}