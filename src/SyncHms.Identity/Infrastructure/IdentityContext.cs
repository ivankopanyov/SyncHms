namespace SyncHms.Identity.Infrastructure;

/// <summary>
/// Класс, описывающий контекст подключения к базе данных.<br/>
/// Унаследован от класса <see cref="IdentityDbContext{TUser, TRole, TKey}"/>
/// </summary>
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
