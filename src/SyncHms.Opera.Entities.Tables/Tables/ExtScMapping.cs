namespace SyncHms.Opera.Entities.Tables;

public partial class ExtScMapping
{
    public string? ExtScStatusCode { get; set; }
    public string? BookControlCode { get; set; }
    public string? OperaStatusCode { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? ExtOrigStatusCode { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ExtScMapping>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("EXT_SC_MAPPING");

            entity.HasIndex(e => e.OperaStatusCode)
                .HasName("EXSM_BS_FKI");

            entity.Property(e => e.BookControlCode)
                .HasColumnName("BOOK_CONTROL_CODE")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ExtOrigStatusCode)
                .HasColumnName("EXT_ORIG_STATUS_CODE")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ExtScStatusCode)
                .IsRequired()
                .HasColumnName("EXT_SC_STATUS_CODE")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.OperaStatusCode)
                .HasColumnName("OPERA_STATUS_CODE")
                .HasMaxLength(10)
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
