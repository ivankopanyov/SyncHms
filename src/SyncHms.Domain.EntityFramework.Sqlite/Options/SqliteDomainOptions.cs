namespace SyncHms.Domain.EntityFramework.Sqlite;

public class SqliteDomainOptions : EntityFrameworkDomainOptions
{
    public const string Section = "SqliteDomain";
    
    public string ConnectionString = "Data Source=app.db";
}