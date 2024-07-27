namespace SyncHms.Opera.Entities.Views;
	
public partial class BofBilltypeTrxcodeTotals
{
    public string? Resort { get; set; }
    public string? BillType { get; set; }
    public string? TrxCode { get; set; }
    public DateTime? TrxDate { get; set; }
    public decimal? TotalGuestDebitAmount { get; set; }
    public decimal? TotalGuestCreditAmount { get; set; }
    public string? TcGroup { get; set; }
    public string? TcSubgroup { get; set; }
    public string? FtSubtype { get; set; }
    public decimal? TotalCalcGrossAmount { get; set; }
    public decimal? TotalGrossAmount { get; set; }
    public decimal? TotalNetAmount { get; set; }
    public decimal? TotalTaxAmount { get; set; }
    public decimal? TotalCovers { get; set; }
    public string? Commission { get; set; }
    public decimal? BofIntfCode { get; set; }
    public string? BofCode1 { get; set; }
    public string? BofCode2 { get; set; }
    public string? BofCode3 { get; set; }
    public string? BofCode4 { get; set; }
    public string? BofCode5 { get; set; }
    public decimal? BofCode6 { get; set; }
    public decimal? BofCode7 { get; set; }
    public decimal? BofCode8 { get; set; }
    public decimal? BofCode9 { get; set; }
    public decimal? BofCode10 { get; set; }
    public DateTime? BofCode11 { get; set; }
    public DateTime? BofCode12 { get; set; }
    public DateTime? BofCode13 { get; set; }
    public DateTime? BofCode14 { get; set; }
    public DateTime? BofCode15 { get; set; }
    public string? BofCode16 { get; set; }
    public string? BofCode17 { get; set; }
    public string? BofCode18 { get; set; }
    public string? BofCode19 { get; set; }
    public string? BofCode20 { get; set; }
    public string? BofCode21 { get; set; }
    public string? BofCode22 { get; set; }
    public string? BofCode23 { get; set; }
    public string? BofCode24 { get; set; }
    public string? BofCode25 { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<BofBilltypeTrxcodeTotals>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("BOF_BILLTYPE_TRXCODE_TOTALS");

            entity.Property(e => e.BillType)
                .HasColumnName("BILL_TYPE")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.BofCode1)
                .HasColumnName("BOF_CODE1")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.BofCode10)
                .HasColumnName("BOF_CODE10")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BofCode11)
                .HasColumnName("BOF_CODE11")
                .HasColumnType("DATE");

            entity.Property(e => e.BofCode12)
                .HasColumnName("BOF_CODE12")
                .HasColumnType("DATE");

            entity.Property(e => e.BofCode13)
                .HasColumnName("BOF_CODE13")
                .HasColumnType("DATE");

            entity.Property(e => e.BofCode14)
                .HasColumnName("BOF_CODE14")
                .HasColumnType("DATE");

            entity.Property(e => e.BofCode15)
                .HasColumnName("BOF_CODE15")
                .HasColumnType("DATE");

            entity.Property(e => e.BofCode16)
                .HasColumnName("BOF_CODE16")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.BofCode17)
                .HasColumnName("BOF_CODE17")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.BofCode18)
                .HasColumnName("BOF_CODE18")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.BofCode19)
                .HasColumnName("BOF_CODE19")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.BofCode2)
                .HasColumnName("BOF_CODE2")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.BofCode20)
                .HasColumnName("BOF_CODE20")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.BofCode21)
                .HasColumnName("BOF_CODE21")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.BofCode22)
                .HasColumnName("BOF_CODE22")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.BofCode23)
                .HasColumnName("BOF_CODE23")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.BofCode24)
                .HasColumnName("BOF_CODE24")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.BofCode25)
                .HasColumnName("BOF_CODE25")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.BofCode3)
                .HasColumnName("BOF_CODE3")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.BofCode4)
                .HasColumnName("BOF_CODE4")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.BofCode5)
                .HasColumnName("BOF_CODE5")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.BofCode6)
                .HasColumnName("BOF_CODE6")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BofCode7)
                .HasColumnName("BOF_CODE7")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BofCode8)
                .HasColumnName("BOF_CODE8")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BofCode9)
                .HasColumnName("BOF_CODE9")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BofIntfCode)
                .HasColumnName("BOF_INTF_CODE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Commission)
                .HasColumnName("COMMISSION")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.FtSubtype)
                .HasColumnName("FT_SUBTYPE")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TcGroup)
                .HasColumnName("TC_GROUP")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TcSubgroup)
                .HasColumnName("TC_SUBGROUP")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TotalCalcGrossAmount)
                .HasColumnName("TOTAL_CALC_GROSS_AMOUNT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TotalCovers)
                .HasColumnName("TOTAL_COVERS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TotalGrossAmount)
                .HasColumnName("TOTAL_GROSS_AMOUNT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TotalGuestCreditAmount)
                .HasColumnName("TOTAL_GUEST_CREDIT_AMOUNT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TotalGuestDebitAmount)
                .HasColumnName("TOTAL_GUEST_DEBIT_AMOUNT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TotalNetAmount)
                .HasColumnName("TOTAL_NET_AMOUNT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TotalTaxAmount)
                .HasColumnName("TOTAL_TAX_AMOUNT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TrxCode)
                .HasColumnName("TRX_CODE")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TrxDate)
                .HasColumnName("TRX_DATE")
                .HasColumnType("DATE");
        });
	}
}
