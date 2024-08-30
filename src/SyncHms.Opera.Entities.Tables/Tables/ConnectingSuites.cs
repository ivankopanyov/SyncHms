namespace SyncHms.Opera.Entities.Tables;

public partial class ConnectingSuites
{
    public string? Resort { get; set; }
    public string? Room { get; set; }
    public string? ConnectingSuiteRoom { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ConnectingSuites>(entity =>
        {
            entity.HasKey(e => new { e.Resort, e.Room, e.ConnectingSuiteRoom })
                .HasName("CONNECTING_SUITES_PK");

            entity.ToTable("CONNECTING_SUITES");

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Room)
                .HasColumnName("ROOM")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ConnectingSuiteRoom)
                .HasColumnName("CONNECTING_SUITE_ROOM")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");
        });
	}
}
