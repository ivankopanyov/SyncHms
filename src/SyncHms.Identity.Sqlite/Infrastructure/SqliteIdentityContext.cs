namespace SyncHms.Identity.Sqlite.Infrastructure;

public class SqliteIdentityContext(SqliteIdentityOptions options, ILogger<SqliteIdentityContext> logger) : IdentityContext
{
    private const int MillisecondsDelay = 1000;
    
    public override int SaveChanges()
    {
        return TrySaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        return TrySaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        return await TrySaveChangesAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        return await TrySaveChangesAsync(cancellationToken, acceptAllChangesOnSuccess);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite(options.ConnectionString)
            .ConfigureWarnings(builder => builder.Ignore(RelationalEventId.AmbientTransactionWarning));
    }

    private int TrySaveChanges(bool? acceptAllChangesOnSuccess = null)
    {
        for (var i = 1; ; i++)
        {
            try
            {
                return acceptAllChangesOnSuccess is { } flag
                    ? base.SaveChanges(flag)
                    : base.SaveChanges();
            }
            catch (SqliteException ex)
            {
                logger.LogError($"Try #{i} {ex.Message}");
                if (ex.SqliteErrorCode != 5)
                    throw;
                
                Thread.Sleep(MillisecondsDelay);
            }
        }
    }

    private async Task<int> TrySaveChangesAsync(CancellationToken cancellationToken,
        bool? acceptAllChangesOnSuccess = null)
    {
        for (var i = 1; ; i++)
        {
            try
            {
                return acceptAllChangesOnSuccess is { } flag
                    ? await base.SaveChangesAsync(flag, cancellationToken)
                    : await base.SaveChangesAsync(cancellationToken);
            }
            catch (SqliteException ex)
            {
                logger.LogError($"Try #{i} {ex.Message}");
                if (ex.SqliteErrorCode != 5)
                    throw;
                
                await Task.Delay(MillisecondsDelay);
            }
        }
    }
}
