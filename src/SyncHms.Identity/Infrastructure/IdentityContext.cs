namespace SyncHms.Identity.Infrastructure;

public abstract class IdentityContext : IdentityDbContext<User, Role, long>
{
    internal IServiceScope? ServiceScope { private get; set; }
    
    protected abstract override void OnConfiguring(DbContextOptionsBuilder optionsBuilder);

    protected sealed override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public sealed override void Dispose()
    {
        base.Dispose();
        ServiceScope?.Dispose();
    }
}
