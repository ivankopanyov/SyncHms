namespace SyncHms.Opera.Entities.Views;
	
public partial class SidMembershipTypesChn
{
    public string? MembershipType { get; set; }
    public string? Description { get; set; }
    public string? MembershipClass { get; set; }
    public string? CardPrefix { get; set; }
    public decimal? CardLength { get; set; }
    public string? CalculationMethod { get; set; }
    public decimal? CalculationMonths { get; set; }
    public decimal? ExpirationMonth { get; set; }
    public string? NumericValidation { get; set; }
    public string? CurrencyCode { get; set; }
    public string? PointsLabel { get; set; }
    public string? FolioMessage { get; set; }
    public decimal? CostPerPoint { get; set; }
    public string? CentralSetupYn { get; set; }
    public decimal? TransactionMaxPoints { get; set; }
    public string? PointsIssuedCentrallyYn { get; set; }
    public string? MembershipAction { get; set; }
    public string? AllowSharesYn { get; set; }
    public string? AllowAdhocMultiplierYn { get; set; }
    public string? UdfCardValidationYn { get; set; }
    public string? AwardGenerationMethod { get; set; }
    public decimal? BatchDelayPeriod { get; set; }
    public DateTime? InactiveDate { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public decimal? OrderBy { get; set; }
    public decimal? GraceExpirationMonth { get; set; }
    public string? CanDeleteYn { get; set; }
    public string? ExchangeRateType { get; set; }
    public decimal? YearsToExpire { get; set; }
    public string? FulfilmentYn { get; set; }
    public string? ExpirationDateRequired { get; set; }
    public string? LevelRequired { get; set; }
    public string? PrimaryMembershipYn { get; set; }
    public string? FolioMessageNonmembers { get; set; }
    public string? UdfFormula { get; set; }
    public decimal? CardValidYears { get; set; }
    public decimal? UpgradePeriod { get; set; }
    public decimal? DowngradePeriod { get; set; }
    public string? AllowDupCardYn { get; set; }
    public string? ExceptionType { get; set; }
    public decimal? MultipleRoomsLimit { get; set; }
    public string? BookerProgramYn { get; set; }
    public string? AutoCardNoBasedOn { get; set; }
    public string? MemberInfoDispSet { get; set; }
    public string? ChainCode { get; set; }
    public string? DefaultMemStatus { get; set; }
    public string? EnrollmentCodeReqYn { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<SidMembershipTypesChn>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("SID_MEMBERSHIP_TYPES_CHN");

            entity.Property(e => e.AllowAdhocMultiplierYn)
                .HasColumnName("ALLOW_ADHOC_MULTIPLIER_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AllowDupCardYn)
                .HasColumnName("ALLOW_DUP_CARD_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AllowSharesYn)
                .HasColumnName("ALLOW_SHARES_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AutoCardNoBasedOn)
                .HasColumnName("AUTO_CARD_NO_BASED_ON")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.AwardGenerationMethod)
                .HasColumnName("AWARD_GENERATION_METHOD")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.BatchDelayPeriod)
                .HasColumnName("BATCH_DELAY_PERIOD")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BookerProgramYn)
                .HasColumnName("BOOKER_PROGRAM_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.CalculationMethod)
                .HasColumnName("CALCULATION_METHOD")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CalculationMonths)
                .HasColumnName("CALCULATION_MONTHS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CanDeleteYn)
                .HasColumnName("CAN_DELETE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.CardLength)
                .HasColumnName("CARD_LENGTH")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CardPrefix)
                .HasColumnName("CARD_PREFIX")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CardValidYears)
                .HasColumnName("CARD_VALID_YEARS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CentralSetupYn)
                .HasColumnName("CENTRAL_SETUP_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ChainCode)
                .IsRequired()
                .HasColumnName("CHAIN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CostPerPoint)
                .HasColumnName("COST_PER_POINT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CurrencyCode)
                .HasColumnName("CURRENCY_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.DefaultMemStatus)
                .HasColumnName("DEFAULT_MEM_STATUS")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.DowngradePeriod)
                .HasColumnName("DOWNGRADE_PERIOD")
                .HasColumnType("NUMBER");

            entity.Property(e => e.EnrollmentCodeReqYn)
                .HasColumnName("ENROLLMENT_CODE_REQ_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ExceptionType)
                .HasColumnName("EXCEPTION_TYPE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.ExchangeRateType)
                .HasColumnName("EXCHANGE_RATE_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ExpirationDateRequired)
                .HasColumnName("EXPIRATION_DATE_REQUIRED")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ExpirationMonth)
                .HasColumnName("EXPIRATION_MONTH")
                .HasColumnType("NUMBER");

            entity.Property(e => e.FolioMessage)
                .HasColumnName("FOLIO_MESSAGE")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.FolioMessageNonmembers)
                .HasColumnName("FOLIO_MESSAGE_NONMEMBERS")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.FulfilmentYn)
                .HasColumnName("FULFILMENT_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.GraceExpirationMonth)
                .HasColumnName("GRACE_EXPIRATION_MONTH")
                .HasColumnType("NUMBER");

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.LevelRequired)
                .HasColumnName("LEVEL_REQUIRED")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.MemberInfoDispSet)
                .HasColumnName("MEMBER_INFO_DISP_SET")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.MembershipAction)
                .HasColumnName("MEMBERSHIP_ACTION")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.MembershipClass)
                .HasColumnName("MEMBERSHIP_CLASS")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.MembershipType)
                .IsRequired()
                .HasColumnName("MEMBERSHIP_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.MultipleRoomsLimit)
                .HasColumnName("MULTIPLE_ROOMS_LIMIT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.NumericValidation)
                .HasColumnName("NUMERIC_VALIDATION")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.PointsIssuedCentrallyYn)
                .HasColumnName("POINTS_ISSUED_CENTRALLY_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.PointsLabel)
                .HasColumnName("POINTS_LABEL")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.PrimaryMembershipYn)
                .HasColumnName("PRIMARY_MEMBERSHIP_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.TransactionMaxPoints)
                .HasColumnName("TRANSACTION_MAX_POINTS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UdfCardValidationYn)
                .HasColumnName("UDF_CARD_VALIDATION_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.UdfFormula)
                .HasColumnName("UDF_FORMULA")
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UpgradePeriod)
                .HasColumnName("UPGRADE_PERIOD")
                .HasColumnType("NUMBER");

            entity.Property(e => e.YearsToExpire)
                .HasColumnName("YEARS_TO_EXPIRE")
                .HasColumnType("NUMBER");
        });
	}
}
