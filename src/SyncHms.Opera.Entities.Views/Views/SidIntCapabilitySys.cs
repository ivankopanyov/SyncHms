namespace SyncHms.Opera.Entities.Views;
	
public partial class SidIntCapabilitySys
{
    public string? InterfaceId { get; set; }
    public string? Resort { get; set; }
    public string? Functionality { get; set; }
    public string? EnabledYn { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<SidIntCapabilitySys>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("SID_INT_CAPABILITY_SYS");

            entity.Property(e => e.EnabledYn)
                .IsRequired()
                .HasColumnName("ENABLED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Functionality)
                .IsRequired()
                .HasColumnName("FUNCTIONALITY")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.InterfaceId)
                .IsRequired()
                .HasColumnName("INTERFACE_ID")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");
        });
	}
}
