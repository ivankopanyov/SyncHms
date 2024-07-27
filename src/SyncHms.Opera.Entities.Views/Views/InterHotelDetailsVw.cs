namespace SyncHms.Opera.Entities.Views;
	
public partial class InterHotelDetailsVw
{
    public decimal? LinkTrxNo { get; set; }
    public string? PostRecFlag { get; set; }
    public decimal? TrxNo { get; set; }
    public DateTime? TrxDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? ToResort { get; set; }
    public string? FromResort { get; set; }
    public DateTime? ReconcileDate { get; set; }
    public decimal? Amount { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<InterHotelDetailsVw>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("INTER_HOTEL_DETAILS_VW");

            entity.Property(e => e.Amount)
                .HasColumnName("AMOUNT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.FromResort)
                .HasColumnName("FROM_RESORT")
                .HasMaxLength(240)
                .IsUnicode(false);

            entity.Property(e => e.LinkTrxNo)
                .HasColumnName("LINK_TRX_NO")
                .HasColumnType("NUMBER");

            entity.Property(e => e.PaymentDate)
                .HasColumnName("PAYMENT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.PostRecFlag)
                .HasColumnName("POST_REC_FLAG")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ReconcileDate)
                .HasColumnName("RECONCILE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.ToResort)
                .HasColumnName("TO_RESORT")
                .HasMaxLength(240)
                .IsUnicode(false);

            entity.Property(e => e.TrxDate)
                .HasColumnName("TRX_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.TrxNo)
                .HasColumnName("TRX_NO")
                .HasColumnType("NUMBER");
        });
	}
}
