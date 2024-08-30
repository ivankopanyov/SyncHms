namespace SyncHms.Opera.Entities.Tables;

public partial class GuaranteeCodes
{
    public string? GuaranteeCode { get; set; }
    public byte? ClosingProbability { get; set; }
    public string? ShortDescription { get; set; }
    public string? LongDescription { get; set; }
    public string? GdsShortDescription { get; set; }
    public string? CashBasisYn { get; set; }
    public string? CroAllowedYn { get; set; }
    public string? PhoneRequiredYn { get; set; }
    public string? AddressRequiredYn { get; set; }
    public string? CreditCardRequiredYn { get; set; }
    public string? MandatoryArrTimeYn { get; set; }
    public string? ReserveInventoryYn { get; set; }
    public string? DepositRequiredYn { get; set; }
    public decimal? OrderBy { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public DateTime? InactiveDate { get; set; }
    public string? BookingStatus { get; set; }
    public string? BookingStatusOrder { get; set; }
    public string? CanDeleteYn { get; set; }
    public string? InternalYn { get; set; }
    public string? ChainCode { get; set; }
    public string? CcVerifyCodeRequiredYn { get; set; }
    public decimal? CcPendingDays { get; set; }
    public string? AutoCancelResYn { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<GuaranteeCodes>(entity =>
        {
            entity.HasKey(e => new { e.ChainCode, e.GuaranteeCode })
                .HasName("GUARANTEE_CODES_PK");

            entity.ToTable("GUARANTEE_CODES");

            entity.Property(e => e.ChainCode)
                .HasColumnName("CHAIN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.GuaranteeCode)
                .HasColumnName("GUARANTEE_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.AddressRequiredYn)
                .HasColumnName("ADDRESS_REQUIRED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AutoCancelResYn)
                .HasColumnName("AUTO_CANCEL_RES_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.BookingStatus)
                .HasColumnName("BOOKING_STATUS")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.BookingStatusOrder)
                .HasColumnName("BOOKING_STATUS_ORDER")
                .HasMaxLength(2)
                .IsUnicode(false);

            entity.Property(e => e.CanDeleteYn)
                .HasColumnName("CAN_DELETE_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CashBasisYn)
                .HasColumnName("CASH_BASIS_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.CcPendingDays)
                .HasColumnName("CC_PENDING_DAYS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CcVerifyCodeRequiredYn)
                .HasColumnName("CC_VERIFY_CODE_REQUIRED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ClosingProbability)
                .HasColumnName("CLOSING_PROBABILITY")
                .HasDefaultValueSql(@"5");

            entity.Property(e => e.CreditCardRequiredYn)
                .HasColumnName("CREDIT_CARD_REQUIRED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.CroAllowedYn)
                .HasColumnName("CRO_ALLOWED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.DepositRequiredYn)
                .HasColumnName("DEPOSIT_REQUIRED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.GdsShortDescription)
                .HasColumnName("GDS_SHORT_DESCRIPTION")
                .HasMaxLength(50)
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

            entity.Property(e => e.InternalYn)
                .HasColumnName("INTERNAL_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.LongDescription)
                .HasColumnName("LONG_DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.MandatoryArrTimeYn)
                .HasColumnName("MANDATORY_ARR_TIME_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql(@"'N'");

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.PhoneRequiredYn)
                .HasColumnName("PHONE_REQUIRED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ReserveInventoryYn)
                .HasColumnName("RESERVE_INVENTORY_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql(@"'N'");

            entity.Property(e => e.ShortDescription)
                .IsRequired()
                .HasColumnName("SHORT_DESCRIPTION")
                .HasMaxLength(80)
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
