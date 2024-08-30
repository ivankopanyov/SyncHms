namespace SyncHms.Opera.Entities.Tables;

public partial class DepositCancelRules
{
    public decimal? RateDcSeq { get; set; }
    public string? Resort { get; set; }
    public decimal? RateSetId { get; set; }
    public string? RateCode { get; set; }
    public string? SeasonCode { get; set; }
    public decimal? DepDaysPriorToArrival { get; set; }
    public decimal? DepDaysAfterBooking { get; set; }
    public decimal? DepAmount { get; set; }
    public string? DepAmountType { get; set; }
    public decimal? CnclDaysPriorToArrival { get; set; }
    public DateTime? CnclBeforeTime { get; set; }
    public decimal? CnclPenltyAmount { get; set; }
    public string? CnclPnltyAmountType { get; set; }
    public string? RuleType { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? RuleCode { get; set; }
    public string? RuleDescription { get; set; }
    public decimal? ParentRateDcSeq { get; set; }
    public string? ReservationType { get; set; }
    public decimal? OrderBy { get; set; }
    public DateTime? InactiveDate { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? OverrideYn { get; set; }
    public decimal? AllotmentHeaderId { get; set; }
    public string? CreditRating { get; set; }
    public string? PaymentRequired { get; set; }
    public string? GuaranteeClass { get; set; }
    public string? Daysofweek { get; set; }
    public string? ApplyRuleTo { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<DepositCancelRules>(entity =>
        {
            entity.HasKey(e => e.RateDcSeq)
                .HasName("DEPOSIT_CANCEL_RULES_PK");

            entity.ToTable("DEPOSIT_CANCEL_RULES");

            entity.HasIndex(e => new { e.SeasonCode, e.Resort })
                .HasName("DCR_SEASONS_IDX");

            entity.HasIndex(e => new { e.Resort, e.RateCode, e.RateSetId })
                .HasName("DCR_RATE_SET_IDX");

            entity.HasIndex(e => new { e.Resort, e.RuleType, e.BeginDate })
                .HasName("DCR_RULETYPE_IDX");

            entity.HasIndex(e => new { e.Resort, e.RateCode, e.RuleType, e.RuleCode })
                .HasName("DCR_RATECODE_IDX");

            entity.HasIndex(e => new { e.Resort, e.ReservationType, e.RuleType, e.RuleCode })
                .HasName("DCR_RESTYPE_IDX");

            entity.Property(e => e.RateDcSeq)
                .HasColumnName("RATE_DC_SEQ")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.AllotmentHeaderId)
                .HasColumnName("ALLOTMENT_HEADER_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ApplyRuleTo)
                .HasColumnName("APPLY_RULE_TO")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.BeginDate)
                .HasColumnName("BEGIN_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CnclBeforeTime)
                .HasColumnName("CNCL_BEFORE_TIME")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CnclDaysPriorToArrival)
                .HasColumnName("CNCL_DAYS_PRIOR_TO_ARRIVAL")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CnclPenltyAmount)
                .HasColumnName("CNCL_PENLTY_AMOUNT")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CnclPnltyAmountType)
                .HasColumnName("CNCL_PNLTY_AMOUNT_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CreditRating)
                .HasColumnName("CREDIT_RATING")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Daysofweek)
                .HasColumnName("DAYSOFWEEK")
                .HasMaxLength(7)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DepAmount)
                .HasColumnName("DEP_AMOUNT")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DepAmountType)
                .HasColumnName("DEP_AMOUNT_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DepDaysAfterBooking)
                .HasColumnName("DEP_DAYS_AFTER_BOOKING")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DepDaysPriorToArrival)
                .HasColumnName("DEP_DAYS_PRIOR_TO_ARRIVAL")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.EndDate)
                .HasColumnName("END_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.GuaranteeClass)
                .HasColumnName("GUARANTEE_CLASS")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.OverrideYn)
                .HasColumnName("OVERRIDE_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ParentRateDcSeq)
                .HasColumnName("PARENT_RATE_DC_SEQ")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PaymentRequired)
                .HasColumnName("PAYMENT_REQUIRED")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RateCode)
                .HasColumnName("RATE_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RateSetId)
                .HasColumnName("RATE_SET_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ReservationType)
                .HasColumnName("RESERVATION_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RuleCode)
                .HasColumnName("RULE_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RuleDescription)
                .HasColumnName("RULE_DESCRIPTION")
                .HasMaxLength(100)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RuleType)
                .IsRequired()
                .HasColumnName("RULE_TYPE")
                .HasMaxLength(10)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SeasonCode)
                .HasColumnName("SEASON_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");
        });
	}
}
