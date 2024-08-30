namespace SyncHms.Opera.Entities.Views;
	
public partial class NameTaxTypes1
{
    public string? Resort { get; set; }
    public string? NameTaxType { get; set; }
    public string? Description { get; set; }
    public string? CollectingAgentYn { get; set; }
    public decimal? ReportExemptDays { get; set; }
    public decimal? ReportTaxPerc { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<NameTaxTypes1>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("NAME_TAX_TYPES");

            entity.Property(e => e.CollectingAgentYn)
                .HasColumnName("COLLECTING_AGENT_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.NameTaxType)
                .IsRequired()
                .HasColumnName("NAME_TAX_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ReportExemptDays)
                .HasColumnName("REPORT_EXEMPT_DAYS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ReportTaxPerc)
                .HasColumnName("REPORT_TAX_PERC")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);
        });
	}
}
