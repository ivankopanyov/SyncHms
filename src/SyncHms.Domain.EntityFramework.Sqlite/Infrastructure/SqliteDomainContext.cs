namespace SyncHms.Domain.EntityFramework.Sqlite.Infrastructure;

public class SqliteDomainContext(SqliteDomainOptions options) : DomainContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(options.ConnectionString);
    }
}
