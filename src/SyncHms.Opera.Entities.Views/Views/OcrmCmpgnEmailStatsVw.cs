namespace SyncHms.Opera.Entities.Views;
	
public partial class OcrmCmpgnEmailStatsVw
{
    public string? CampaignCode { get; set; }
    public decimal? EmailSend { get; set; }
    public decimal? OpenEmail { get; set; }
    public decimal? ClicksOnEmail { get; set; }
    public decimal? Bounce { get; set; }
    public decimal? SoftBounce { get; set; }
    public decimal? HardBounce { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<OcrmCmpgnEmailStatsVw>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("OCRM_CMPGN_EMAIL_STATS_VW");

            entity.Property(e => e.Bounce)
                .HasColumnName("BOUNCE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CampaignCode)
                .IsRequired()
                .HasColumnName("CAMPAIGN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ClicksOnEmail)
                .HasColumnName("CLICKS_ON_EMAIL")
                .HasColumnType("NUMBER");

            entity.Property(e => e.EmailSend)
                .HasColumnName("EMAIL_SEND")
                .HasColumnType("NUMBER");

            entity.Property(e => e.HardBounce)
                .HasColumnName("HARD_BOUNCE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.OpenEmail)
                .HasColumnName("OPEN_EMAIL")
                .HasColumnType("NUMBER");

            entity.Property(e => e.SoftBounce)
                .HasColumnName("SOFT_BOUNCE")
                .HasColumnType("NUMBER");
        });
	}
}
