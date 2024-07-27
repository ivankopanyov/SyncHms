namespace SyncHms.Opera.Entities.Views;
	
public partial class SidOcrmEspProvidersSys
{
    public string? EspCode { get; set; }
    public string? EspName { get; set; }
    public string? EspDescription { get; set; }
    public string? CanDeleteYn { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? DefaultQCode { get; set; }
    public DateTime? InactiveDate { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<SidOcrmEspProvidersSys>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("SID_OCRM_ESP_PROVIDERS_SYS");

            entity.Property(e => e.CanDeleteYn)
                .HasColumnName("CAN_DELETE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.DefaultQCode)
                .HasColumnName("DEFAULT_Q_CODE")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.EspCode)
                .IsRequired()
                .HasColumnName("ESP_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.EspDescription)
                .HasColumnName("ESP_DESCRIPTION")
                .IsUnicode(false);

            entity.Property(e => e.EspName)
                .HasColumnName("ESP_NAME")
                .HasMaxLength(400)
                .IsUnicode(false);

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE");

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
