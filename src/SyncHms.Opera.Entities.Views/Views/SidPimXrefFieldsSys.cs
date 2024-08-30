namespace SyncHms.Opera.Entities.Views;
	
public partial class SidPimXrefFieldsSys
{
    public string? PimCode { get; set; }
    public string? OperaType { get; set; }
    public string? OperaTable { get; set; }
    public string? OperaField { get; set; }
    public string? PimField { get; set; }
    public string? PkField { get; set; }
    public string? UpdateTable { get; set; }
    public string? UpdateField { get; set; }
    public string? UpdatePkField { get; set; }
    public string? FieldDatatype { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? Version { get; set; }
    public decimal? OrderBy { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<SidPimXrefFieldsSys>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("SID_PIM_XREF_FIELDS_SYS");

            entity.Property(e => e.FieldDatatype)
                .HasColumnName("FIELD_DATATYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.OperaField)
                .IsRequired()
                .HasColumnName("OPERA_FIELD")
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.OperaTable)
                .IsRequired()
                .HasColumnName("OPERA_TABLE")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.OperaType)
                .IsRequired()
                .HasColumnName("OPERA_TYPE")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.PimCode)
                .IsRequired()
                .HasColumnName("PIM_CODE")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.PimField)
                .IsRequired()
                .HasColumnName("PIM_FIELD")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.PkField)
                .IsRequired()
                .HasColumnName("PK_FIELD")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateField)
                .HasColumnName("UPDATE_FIELD")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.UpdatePkField)
                .HasColumnName("UPDATE_PK_FIELD")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.UpdateTable)
                .HasColumnName("UPDATE_TABLE")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Version)
                .HasColumnName("VERSION")
                .HasMaxLength(50)
                .IsUnicode(false);
        });
	}
}
