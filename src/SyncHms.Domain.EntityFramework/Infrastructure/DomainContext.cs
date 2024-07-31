namespace SyncHms.Domain.EntityFramework.Infrastructure;

public abstract class DomainContext : DbContext
{
    internal IServiceScope? ServiceScope { private get; set; }

    public virtual DbSet<Service> Services { get; set; }
    
    public virtual DbSet<AppOptions> AppOptions { get; set; }

    public virtual DbSet<Log> Logs { get; set; }
    
    public virtual DbSet<LogData> LogDatas { get; set; }

    protected abstract override void OnConfiguring(DbContextOptionsBuilder optionsBuilder);

    protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>(buildAction =>
        {
            buildAction.HasKey(s => s.Name);
            buildAction.Ignore(s => s.State);
        });
        
        modelBuilder.Entity<AppOptions>(buildAction =>
        {
            buildAction.HasKey(ao => ao.Id);
        });
        
        modelBuilder.Entity<Log>(buildAction =>
        {
            buildAction.HasKey(l => l.Id);

            buildAction
                .Property(l => l.Id)
                .ValueGeneratedOnAdd();

            buildAction
                .HasOne(l => l.LogData)
                .WithOne(ld => ld.Log)
                .HasForeignKey<LogData>(ld => ld.LogId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<LogData>(buildAction =>
        {
            buildAction.HasKey(ld => ld.Id);

            buildAction
                .Property(ld => ld.Id)
                .ValueGeneratedOnAdd();

            buildAction
                .HasOne(ld => ld.Log)
                .WithOne(l => l.LogData)
                .HasForeignKey<Log>(l => l.LogDataId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    public sealed override void Dispose()
    {
        base.Dispose();
        ServiceScope?.Dispose();
    }
}
