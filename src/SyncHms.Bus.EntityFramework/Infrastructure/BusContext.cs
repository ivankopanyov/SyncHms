namespace SyncHms.Bus.EntityFramework;

/// <summary>
/// Класс, описывающий контекст подключения к базе данных шины.<br/>
/// Унаследован от <see cref="DbContext"/>
/// </summary>
public abstract class BusContext : DbContext
{
    internal IServiceScope? ServiceScope { private get; set; }

    /// <summary>Коллекция хранилищ сообщений шины данных.</summary>
    public virtual DbSet<Exchange> Exchanges { get; set; }

    /// <summary>Коллекция очередей сообщений шины данных.</summary>
    public virtual DbSet<Queue> Queues { get; set; }


    /// <summary>Коллекция сообщений шины данных.</summary>
    public virtual DbSet<Message> Messages { get; set; }

    protected abstract override void OnConfiguring(DbContextOptionsBuilder optionsBuilder);

    protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Exchange>(buildAction =>
        {
            buildAction.HasKey(e => e.Name);

            buildAction
                .HasMany(e => e.Queues)
                .WithOne(q => q.Exchange)
                .HasForeignKey(q => q.ExchangeName)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Queue>(buildAction =>
        {
            buildAction.HasKey(q => new { q.Name, q.ExchangeName });

            buildAction
                .HasOne(q => q.Exchange)
                .WithMany(e => e.Queues)
                .HasForeignKey(q => q.ExchangeName)
                .OnDelete(DeleteBehavior.Cascade);

            buildAction
                .HasMany(q => q.Messages)
                .WithOne(m => m.Queue)
                .HasForeignKey(m => new { m.QueueName, m.ExchangeName })
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Message>(buildAction =>
        {
            buildAction.HasKey(m => m.Id);

            buildAction
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            buildAction
                .HasOne(m => m.Queue)
                .WithMany(q => q.Messages)
                .HasForeignKey(m => new { m.QueueName, m.ExchangeName })
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    public sealed override void Dispose()
    {
        base.Dispose();
        ServiceScope?.Dispose();
    }
}
