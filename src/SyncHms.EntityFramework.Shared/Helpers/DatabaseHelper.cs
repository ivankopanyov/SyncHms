namespace SyncHms.EntityFramework.Shared;

public static class DatabaseHelper
{
    public static void InitializeDatabase<TContext>(TContext context, InitDatabase initDatabase) where TContext : DbContext
    {
        switch (initDatabase)
        {
            case InitDatabase.Migrate:
                context.Database.Migrate();
                break;
            case InitDatabase.EnsureCreated:
            default:
                context.Database.EnsureCreated();
                break;
        }
    }
}