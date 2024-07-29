namespace SyncHms.Identity.Sqlite.Infrastructure;

public class SqliteIdentityContext(SqliteIdentityOptions options) : IdentityContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(options.ConnectionString);
    }
}
