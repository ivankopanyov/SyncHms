namespace SyncHms.Opera.Entities.Views;
	
public partial class SidMenuUrls
{
    public string? Resort { get; set; }
    public string? FormModule { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public string? LicenseCode { get; set; }
    public string? ParameterName { get; set; }
    public string? ConfigMode { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? InactiveMode { get; set; }
    public string? BrowserWindowProperties { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<SidMenuUrls>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("SID_MENU$_URLS");

            entity.Property(e => e.BrowserWindowProperties)
                .HasColumnName("BROWSER_WINDOW_PROPERTIES")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.ConfigMode)
                .HasColumnName("CONFIG_MODE")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.FormModule)
                .IsRequired()
                .HasColumnName("FORM_MODULE")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.InactiveMode)
                .HasColumnName("INACTIVE_MODE")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.LicenseCode)
                .HasColumnName("LICENSE_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ParameterName)
                .HasColumnName("PARAMETER_NAME")
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

            entity.Property(e => e.Url)
                .HasColumnName("URL")
                .IsUnicode(false);
        });
	}
}
