namespace SyncHms.Opera.Entities.Views;
	
public partial class SidResortFacilities
{
    public string? Resort { get; set; }
    public string? FacilityCode { get; set; }
    public string? Description { get; set; }
    public decimal? OrderBy { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<SidResortFacilities>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("SID_RESORT_FACILITIES");

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.FacilityCode)
                .IsRequired()
                .HasColumnName("FACILITY_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");

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
