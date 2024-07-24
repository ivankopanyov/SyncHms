namespace SyncHms.Bus.EntityFramework.Infrastructure;

internal class EntityFrameworkBusContext(EntityFrameworkBusOptions options) : DbContext
{
    public virtual DbSet<Exchange> Exchanges { get; set; }

    public virtual DbSet<Queue> Queues { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        options.OptionsBuilder?.Invoke(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
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
}
