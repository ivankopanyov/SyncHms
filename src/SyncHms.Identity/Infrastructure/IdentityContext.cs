namespace SyncHms.Identity.Infrastructure;

internal class IdentityContext(IdentityOptions options) : IdentityDbContext<User, Role, long>
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        options.OptionsBuilder?.Invoke(optionsBuilder);
    }
}
