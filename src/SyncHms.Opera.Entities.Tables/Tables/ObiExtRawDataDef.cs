namespace SyncHms.Opera.Entities.Tables;

public partial class ObiExtRawDataDef
{
    public string? DatamartName { get; set; }
    public string? RawDataName { get; set; }
    public string? Description { get; set; }
    public string? ExtName { get; set; }
    public string? OperaTableName { get; set; }
    public string? FileNameLike { get; set; }
    public string? XmlFormatYn { get; set; }
    public string? ExtHdrName { get; set; }
    public decimal? OrderBy { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? BiTableYn { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ObiExtRawDataDef>(entity =>
        {
            entity.HasKey(e => new { e.DatamartName, e.RawDataName })
                .HasName("OBI_EXT_RAW_DATA_PK");

            entity.ToTable("OBI_EXT_RAW_DATA_DEF");

            entity.Property(e => e.DatamartName)
                .HasColumnName("DATAMART_NAME")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.RawDataName)
                .HasColumnName("RAW_DATA_NAME")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.BiTableYn)
                .HasColumnName("BI_TABLE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .IsUnicode(false);

            entity.Property(e => e.ExtHdrName)
                .HasColumnName("EXT_HDR_NAME")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.ExtName)
                .HasColumnName("EXT_NAME")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.FileNameLike)
                .HasColumnName("FILE_NAME_LIKE")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.OperaTableName)
                .HasColumnName("OPERA_TABLE_NAME")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.XmlFormatYn)
                .HasColumnName("XML_FORMAT_YN")
                .HasMaxLength(1)
                .IsUnicode(false);
        });
	}
}
