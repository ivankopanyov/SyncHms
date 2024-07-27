namespace SyncHms.Opera.Entities.Views;
	
public partial class RepBeoRevSumDisc
{
    public string? Resort { get; set; }
    public decimal? BookId { get; set; }
    public decimal? EventId { get; set; }
    public DateTime? StartDate { get; set; }
    public string? RevenueType { get; set; }
    public string? RevenueDesc { get; set; }
    public decimal? Revenue { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<RepBeoRevSumDisc>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("REP_BEO_REV_SUM_DISC");

            entity.Property(e => e.BookId)
                .HasColumnName("BOOK_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.EventId)
                .HasColumnName("EVENT_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Revenue)
                .HasColumnName("REVENUE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.RevenueDesc)
                .HasColumnName("REVENUE_DESC")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.RevenueType)
                .HasColumnName("REVENUE_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.StartDate)
                .HasColumnName("START_DATE")
                .HasColumnType("DATE");
        });
	}
}
