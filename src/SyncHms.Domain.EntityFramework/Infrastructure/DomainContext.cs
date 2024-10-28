namespace SyncHms.Domain.EntityFramework.Infrastructure;

/// <summary>
/// Класс, описывающий контекст подключения к базе данных.<br/>
/// Унаследован от класса <see cref="DbContext"/>
/// </summary>
public abstract class DomainContext : DbContext
{
    internal IServiceScope? ServiceScope { private get; set; }
    
    public virtual DbSet<Service> Services { get; set; }
    
    public virtual DbSet<AppOptions> AppOptions { get; set; }

    public virtual DbSet<Log> Logs { get; set; }
    
    public virtual DbSet<LogData> LogDatas { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

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

        modelBuilder.Entity<Schedule>(buildAction =>
        {
            buildAction.HasKey(s => s.Name);
        });
    }

    public sealed override void Dispose()
    {
        base.Dispose();
        ServiceScope?.Dispose();
    }
}
