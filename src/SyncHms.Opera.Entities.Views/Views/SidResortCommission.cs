namespace SyncHms.Opera.Entities.Views;
	
public partial class SidResortCommission
{
    public string? Resort { get; set; }
    public string? CommissionCode { get; set; }
    public string? Description { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? InactiveDate { get; set; }
    public decimal? SellSequence { get; set; }
    public decimal? VatAmount { get; set; }
    public string? CommissionFlatPercentage { get; set; }
    public string? CommissionCalcRule { get; set; }
    public decimal? Amount { get; set; }
    public string? PerNight { get; set; }
    public string? PerStay { get; set; }
    public string? CommType { get; set; }
    public string? HoldArYn { get; set; }
    public string? HoldPrepaidYn { get; set; }
    public string? HoldAlwaysYn { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<SidResortCommission>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("SID_RESORT_COMMISSION");

            entity.Property(e => e.Amount)
                .HasColumnName("AMOUNT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CommType)
                .HasColumnName("COMM_TYPE")
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.Property(e => e.CommissionCalcRule)
                .HasColumnName("COMMISSION_CALC_RULE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CommissionCode)
                .IsRequired()
                .HasColumnName("COMMISSION_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CommissionFlatPercentage)
                .HasColumnName("COMMISSION_FLAT_PERCENTAGE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.HoldAlwaysYn)
                .HasColumnName("HOLD_ALWAYS_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.HoldArYn)
                .HasColumnName("HOLD_AR_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.HoldPrepaidYn)
                .HasColumnName("HOLD_PREPAID_YN")
                .HasMaxLength(1)
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

            entity.Property(e => e.PerNight)
                .HasColumnName("PER_NIGHT")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.PerStay)
                .HasColumnName("PER_STAY")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.SellSequence)
                .HasColumnName("SELL_SEQUENCE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.VatAmount)
                .HasColumnName("VAT_AMOUNT")
                .HasColumnType("NUMBER");
        });
	}
}
