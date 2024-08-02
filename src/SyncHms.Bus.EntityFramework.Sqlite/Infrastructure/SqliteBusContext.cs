namespace SyncHms.Bus.EntityFramework.Sqlite.Infrastructure;

public class SqliteBusContext(SqliteBusOptions options) : BusContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite(options.ConnectionString)
            .ConfigureWarnings(builder => builder.Ignore(RelationalEventId.AmbientTransactionWarning));
    }
}
