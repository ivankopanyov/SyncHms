namespace SyncHms.Opera.Entities.Views;
	
public partial class ExpArAccounts
{
    public string? Resort { get; set; }
    public decimal? AccountCode { get; set; }
    public string? AccountNo { get; set; }
    public decimal? AccountTypeId { get; set; }
    public string? AccountName { get; set; }
    public string? CompanyName { get; set; }
    public string? AccountSname { get; set; }
    public string? Status { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? Zip { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Contact { get; set; }
    public string? BatchStmtYn { get; set; }
    public string? PermAcctYn { get; set; }
    public string? SumCurCode { get; set; }
    public decimal? CreditLimit { get; set; }
    public DateTime? LstRemSent { get; set; }
    public DateTime? LstStmtSent { get; set; }
    public string? LstRemText { get; set; }
    public decimal? Age { get; set; }
    public string? Remarks { get; set; }
    public decimal? NameId { get; set; }
    public DateTime? InactiveDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? LastStmtFaxNo { get; set; }
    public string? LastRemFaxNo { get; set; }
    public decimal? NoOfPersons { get; set; }
    public decimal? Balance { get; set; }
    public DateTime? LstRemPrtDate { get; set; }
    public string? AccountStatus { get; set; }
    public string? EmailAddress { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ExpArAccounts>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("EXP_AR_ACCOUNTS");

            entity.Property(e => e.AccountCode)
                .HasColumnName("ACCOUNT_CODE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.AccountName)
                .IsRequired()
                .HasColumnName("ACCOUNT_NAME")
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.Property(e => e.AccountNo)
                .HasColumnName("ACCOUNT_NO")
                .IsUnicode(false);

            entity.Property(e => e.AccountSname)
                .HasColumnName("ACCOUNT_SNAME")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.AccountStatus)
                .HasColumnName("ACCOUNT_STATUS")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccountTypeId)
                .HasColumnName("ACCOUNT_TYPE_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Address1)
                .HasColumnName("ADDRESS1")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.Address2)
                .HasColumnName("ADDRESS2")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.Address3)
                .HasColumnName("ADDRESS3")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.Age)
                .HasColumnName("AGE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Balance)
                .HasColumnName("BALANCE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BatchStmtYn)
                .HasColumnName("BATCH_STMT_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.City)
                .HasColumnName("CITY")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.CompanyName)
                .HasColumnName("COMPANY_NAME")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.Contact)
                .HasColumnName("CONTACT")
                .IsUnicode(false);

            entity.Property(e => e.Country)
                .HasColumnName("COUNTRY")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CreditLimit)
                .HasColumnName("CREDIT_LIMIT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.EmailAddress)
                .HasColumnName("EMAIL_ADDRESS")
                .IsUnicode(false);

            entity.Property(e => e.Fax)
                .HasColumnName("FAX")
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

            entity.Property(e => e.LastRemFaxNo)
                .HasColumnName("LAST_REM_FAX_NO")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.LastStmtFaxNo)
                .HasColumnName("LAST_STMT_FAX_NO")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.LstRemPrtDate)
                .HasColumnName("LST_REM_PRT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.LstRemSent)
                .HasColumnName("LST_REM_SENT")
                .HasColumnType("DATE");

            entity.Property(e => e.LstRemText)
                .HasColumnName("LST_REM_TEXT")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.LstStmtSent)
                .HasColumnName("LST_STMT_SENT")
                .HasColumnType("DATE");

            entity.Property(e => e.NameId)
                .HasColumnName("NAME_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.NoOfPersons)
                .HasColumnName("NO_OF_PERSONS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.PermAcctYn)
                .HasColumnName("PERM_ACCT_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Phone)
                .HasColumnName("PHONE")
                .IsUnicode(false);

            entity.Property(e => e.Remarks)
                .HasColumnName("REMARKS")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.State)
                .HasColumnName("STATE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.Status)
                .HasColumnName("STATUS")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.SumCurCode)
                .HasColumnName("SUM_CUR_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Zip)
                .HasColumnName("ZIP")
                .HasMaxLength(15)
                .IsUnicode(false);
        });
	}
}
