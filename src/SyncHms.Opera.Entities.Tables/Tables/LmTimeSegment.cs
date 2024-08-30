namespace SyncHms.Opera.Entities.Tables;

public partial class LmTimeSegment
{
    public string? TimePatternCode { get; set; }
    public string? TimeSegmentCode { get; set; }
    public string? Resort { get; set; }
    public string? BeginTimeHh24mi { get; set; }
    public string? EndTimeHh24mi { get; set; }
    public decimal? OrderBy { get; set; }
    public DateTime? InactiveDate { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<LmTimeSegment>(entity =>
        {
            entity.HasKey(e => new { e.TimePatternCode, e.TimeSegmentCode, e.Resort })
                .HasName("LM_TIME_SEGMENT_PK");

            entity.ToTable("LM_TIME_SEGMENT");

            entity.Property(e => e.TimePatternCode)
                .HasColumnName("TIME_PATTERN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.TimeSegmentCode)
                .HasColumnName("TIME_SEGMENT_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.BeginTimeHh24mi)
                .IsRequired()
                .HasColumnName("BEGIN_TIME_HH24MI")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.EndTimeHh24mi)
                .IsRequired()
                .HasColumnName("END_TIME_HH24MI")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();
        });
	}
}
