namespace SyncHms.Opera.Entities.Tables;

public partial class ImpFileHdr
{
    public string? ImpFileType { get; set; }
    public string? ImpDesc { get; set; }
    public string? ImpTable { get; set; }
    public string? ImpCntrlName { get; set; }
    public string? ImpPreProcess { get; set; }
    public string? ImpProcess { get; set; }
    public string? InactiveYn { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? UsedInApp { get; set; }
    public string? ConfigMode { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ImpFileHdr>(entity =>
        {
            entity.HasKey(e => e.ImpFileType)
                .HasName("IFR_PK");

            entity.ToTable("IMP_FILE_HDR");

            entity.Property(e => e.ImpFileType)
                .HasColumnName("IMP_FILE_TYPE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.ConfigMode)
                .HasColumnName("CONFIG_MODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ImpCntrlName)
                .HasColumnName("IMP_CNTRL_NAME")
                .IsUnicode(false);

            entity.Property(e => e.ImpDesc)
                .HasColumnName("IMP_DESC")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.ImpPreProcess)
                .HasColumnName("IMP_PRE_PROCESS")
                .IsUnicode(false);

            entity.Property(e => e.ImpProcess)
                .HasColumnName("IMP_PROCESS")
                .IsUnicode(false);

            entity.Property(e => e.ImpTable)
                .HasColumnName("IMP_TABLE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.InactiveYn)
                .HasColumnName("INACTIVE_YN")
                .HasMaxLength(1)
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

            entity.Property(e => e.UsedInApp)
                .HasColumnName("USED_IN_APP")
                .HasMaxLength(20)
                .IsUnicode(false);
        });
	}
}
