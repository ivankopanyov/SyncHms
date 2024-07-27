namespace SyncHms.Opera.Entities.Views;
	
public partial class OperaEcertsAllocVw
{
    public decimal? CertId { get; set; }
    public string? VoucherNo { get; set; }
    public string? CertCode { get; set; }
    public string? ChainCode { get; set; }
    public string? CertificateNo { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? ShortDescription { get; set; }
    public string? CertLabel { get; set; }
    public string? ComputedStatus { get; set; }
    public decimal? NameId { get; set; }
    public string? VoucherBenefitCode { get; set; }
    public DateTime? InactiveDate { get; set; }
    public decimal? MembershipAwardId { get; set; }
    public string? ConsumptionRefType { get; set; }
    public string? ConsumptionLegNo { get; set; }
    public string? IssuedDatabaseId { get; set; }
    public string? IssueSource { get; set; }
    public string? VerifyCertNoYn { get; set; }
    public string? IssueType { get; set; }
    public decimal? AwardPoints { get; set; }
    public decimal? ResvNameId { get; set; }
    public string? AuthCode { get; set; }
    public decimal? GenRuleId { get; set; }
    public string? GenRuleValue { get; set; }
    public string? GenRuleValueType { get; set; }
    public string? OriginalVoucherNo { get; set; }
    public string? OriginalCertificateNo { get; set; }
    public decimal? CertificateSeqNo { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ServiceDate { get; set; }
    public DateTime? CancelRequestDate { get; set; }
    public string? CancelAllowedYn { get; set; }
    public string? GenInLieuCertYn { get; set; }
    public string? ReservationCertYn { get; set; }
    public string? MembershipType { get; set; }
    public string? AwardType { get; set; }
    public string? PromotionCode { get; set; }
    public string? Resort { get; set; }
    public string? ConsumedAt { get; set; }
    public string? ConsumerLastName { get; set; }
    public string? ConsumerFirstName { get; set; }
    public string? ConsumerMiddleName { get; set; }
    public string? ConsumerEmail { get; set; }
    public DateTime? ConsumptionDate { get; set; }
    public string? ConsumptionUserName { get; set; }
    public string? BenefitSummary { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<OperaEcertsAllocVw>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("OPERA_ECERTS_ALLOC_VW");

            entity.Property(e => e.AuthCode)
                .HasColumnName("AUTH_CODE")
                .HasMaxLength(120)
                .IsUnicode(false);

            entity.Property(e => e.AwardPoints)
                .HasColumnName("AWARD_POINTS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.AwardType)
                .HasColumnName("AWARD_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.BenefitSummary)
                .HasColumnName("BENEFIT_SUMMARY")
                .IsUnicode(false);

            entity.Property(e => e.CancelAllowedYn)
                .HasColumnName("CANCEL_ALLOWED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.CancelRequestDate)
                .HasColumnName("CANCEL_REQUEST_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.CertCode)
                .IsRequired()
                .HasColumnName("CERT_CODE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.CertId)
                .HasColumnName("CERT_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CertLabel)
                .HasColumnName("CERT_LABEL")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.CertificateNo)
                .HasColumnName("CERTIFICATE_NO")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.CertificateSeqNo)
                .HasColumnName("CERTIFICATE_SEQ_NO")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ChainCode)
                .IsRequired()
                .HasColumnName("CHAIN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ComputedStatus)
                .HasColumnName("COMPUTED_STATUS")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ConsumedAt)
                .HasColumnName("CONSUMED_AT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ConsumerEmail)
                .HasColumnName("CONSUMER_EMAIL")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.ConsumerFirstName)
                .HasColumnName("CONSUMER_FIRST_NAME")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.ConsumerLastName)
                .HasColumnName("CONSUMER_LAST_NAME")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.ConsumerMiddleName)
                .HasColumnName("CONSUMER_MIDDLE_NAME")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.ConsumptionDate)
                .HasColumnName("CONSUMPTION_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.ConsumptionLegNo)
                .HasColumnName("CONSUMPTION_LEG_NO")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.ConsumptionRefType)
                .HasColumnName("CONSUMPTION_REF_TYPE")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ConsumptionUserName)
                .HasColumnName("CONSUMPTION_USER_NAME")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.ExpiryDate)
                .HasColumnName("EXPIRY_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.GenInLieuCertYn)
                .HasColumnName("GEN_IN_LIEU_CERT_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.GenRuleId)
                .HasColumnName("GEN_RULE_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.GenRuleValue)
                .HasColumnName("GEN_RULE_VALUE")
                .IsUnicode(false);

            entity.Property(e => e.GenRuleValueType)
                .HasColumnName("GEN_RULE_VALUE_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.IssueDate)
                .HasColumnName("ISSUE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.IssueSource)
                .HasColumnName("ISSUE_SOURCE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.IssueType)
                .HasColumnName("ISSUE_TYPE")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.IssuedDatabaseId)
                .HasColumnName("ISSUED_DATABASE_ID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.MembershipAwardId)
                .HasColumnName("MEMBERSHIP_AWARD_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MembershipType)
                .HasColumnName("MEMBERSHIP_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.NameId)
                .HasColumnName("NAME_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.OriginalCertificateNo)
                .HasColumnName("ORIGINAL_CERTIFICATE_NO")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.OriginalVoucherNo)
                .HasColumnName("ORIGINAL_VOUCHER_NO")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.PromotionCode)
                .HasColumnName("PROMOTION_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ReservationCertYn)
                .HasColumnName("RESERVATION_CERT_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ResvNameId)
                .HasColumnName("RESV_NAME_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ServiceDate)
                .HasColumnName("SERVICE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.ShortDescription)
                .HasColumnName("SHORT_DESCRIPTION")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.VerifyCertNoYn)
                .HasColumnName("VERIFY_CERT_NO_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.VoucherBenefitCode)
                .HasColumnName("VOUCHER_BENEFIT_CODE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.VoucherNo)
                .IsRequired()
                .HasColumnName("VOUCHER_NO")
                .HasMaxLength(40)
                .IsUnicode(false);
        });
	}
}
