namespace SyncHms.Opera.Entities.Views;
	
public partial class SidGdsConvErrorCodesSys
{
    public string? GdsHost { get; set; }
    public string? ErrorCode { get; set; }
    public string? GdsErrorCodePrefix { get; set; }
    public string? GdsErrorCodeSuffix { get; set; }
    public string? Description { get; set; }
    public string? BookingResponse { get; set; }
    public string? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? ManualProcessYn { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<SidGdsConvErrorCodesSys>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("SID_GDS_CONV_ERROR_CODES_SYS");

            entity.Property(e => e.BeginDate)
                .HasColumnName("BEGIN_DATE")
                .HasMaxLength(33)
                .IsUnicode(false);

            entity.Property(e => e.BookingResponse)
                .IsRequired()
                .HasColumnName("BOOKING_RESPONSE")
                .HasMaxLength(2)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.EndDate)
                .HasColumnName("END_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.ErrorCode)
                .IsRequired()
                .HasColumnName("ERROR_CODE")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.GdsErrorCodePrefix)
                .IsRequired()
                .HasColumnName("GDS_ERROR_CODE_PREFIX")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.GdsErrorCodeSuffix)
                .IsRequired()
                .HasColumnName("GDS_ERROR_CODE_SUFFIX")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.GdsHost)
                .IsRequired()
                .HasColumnName("GDS_HOST")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ManualProcessYn)
                .HasColumnName("MANUAL_PROCESS_YN")
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
