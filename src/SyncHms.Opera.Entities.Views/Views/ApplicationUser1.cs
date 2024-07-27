namespace SyncHms.Opera.Entities.Views;
	
public partial class ApplicationUser1
{
    public decimal? AppUserId { get; set; }
    public string? AppUser { get; set; }
    public string? Name { get; set; }
    public string? AppPassword { get; set; }
    public decimal? OracleUid { get; set; }
    public string? OracleUser { get; set; }
    public string? OraclePassword { get; set; }
    public DateTime? InactiveDate { get; set; }
    public string? Title { get; set; }
    public string? DefaultForm { get; set; }
    public string? AppUserType { get; set; }
    public string? LastLoggedResort { get; set; }
    public decimal? DefCashierId { get; set; }
    public string? AppUserDescription { get; set; }
    public decimal? PersonNameId { get; set; }
    public DateTime? DisabledUntil { get; set; }
    public DateTime? ExpiresOn { get; set; }
    public DateTime? LastLoggedTimestamp { get; set; }
    public DateTime? PasswordLastChange { get; set; }
    public DateTime? PasswordNextChange { get; set; }
    public decimal? PasswordChangeDays { get; set; }
    public decimal? GraceLogin { get; set; }
    public string? IsSuperuser { get; set; }
    public string? EmployeeNumber { get; set; }
    public string? DefaultResort { get; set; }
    public string? GeneralFilepath { get; set; }
    public string? UserFilepath { get; set; }
    public decimal? MaxUserSessions { get; set; }
    public decimal? MaxCheckoutDays { get; set; }
    public string? DefaultTerminal { get; set; }
    public string? DefaultLanguage { get; set; }
    public string? DeptId { get; set; }
    public string? MaleFemale { get; set; }
    public decimal? UserPbxId { get; set; }
    public DateTime? DateHired { get; set; }
    public string? WorkPermitNo { get; set; }
    public DateTime? WorkPermitExpdate { get; set; }
    public string? RateType { get; set; }
    public string? SalaryInterval { get; set; }
    public decimal? HourlyRate { get; set; }
    public decimal? WeeklySalary { get; set; }
    public decimal? OtMultiplier { get; set; }
    public string? HireType { get; set; }
    public string? RehireYn { get; set; }
    public string? EmpExtension { get; set; }
    public string? EmpPager { get; set; }
    public string? TermReason { get; set; }
    public DateTime? TerminatedDate { get; set; }
    public string? InactiveReasonCode { get; set; }
    public DateTime? InactiveFrom { get; set; }
    public DateTime? InactiveTo { get; set; }
    public decimal? WeekMin { get; set; }
    public decimal? WeekMax { get; set; }
    public decimal? MondayMin { get; set; }
    public decimal? MondayMax { get; set; }
    public decimal? TuesdayMin { get; set; }
    public decimal? TuesdayMax { get; set; }
    public decimal? WednesdayMin { get; set; }
    public decimal? WednesdayMax { get; set; }
    public decimal? ThursdayMin { get; set; }
    public decimal? ThursdayMax { get; set; }
    public decimal? FridayMin { get; set; }
    public decimal? FridayMax { get; set; }
    public decimal? SaturdayMin { get; set; }
    public decimal? SaturdayMax { get; set; }
    public decimal? SundayMin { get; set; }
    public decimal? SundayMax { get; set; }
    public string? Comments { get; set; }
    public string? LeadAddress { get; set; }
    public string? LeadComm { get; set; }
    public string? LeadAddressDet { get; set; }
    public string? EmpStatus { get; set; }
    public decimal? HoursPerWeek { get; set; }
    public decimal? LaptopId { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? AuthorizerYn { get; set; }
    public string? SfaName { get; set; }
    public string? LoginCro { get; set; }
    public string? MfnUserType { get; set; }
    public string? ForcePasswordChangeYn { get; set; }
    public string? AccountLockedOutYn { get; set; }
    public string? PreventAccountLockout { get; set; }
    public DateTime? LockoutDate { get; set; }
    public string? AccessPms { get; set; }
    public string? AccessSc { get; set; }
    public string? AccessConfig { get; set; }
    public string? AccessEod { get; set; }
    public string? AccessUtil { get; set; }
    public string? AccessOrs { get; set; }
    public string? AccessSfa { get; set; }
    public string? AccessOcis { get; set; }
    public string? AccessOcm { get; set; }
    public string? AccessOxi { get; set; }
    public string? AccessOxihub { get; set; }
    public decimal? MaxDaysAfterCo { get; set; }
    public string? UserGroupAdmin { get; set; }
    public string? AccessOrms { get; set; }
    public string? AccessObi { get; set; }
    public string? AccessScbi { get; set; }
    public decimal? LoginAttempts { get; set; }
    public string? PropertyAccessYn { get; set; }
    public string? AccessOcrm { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ApplicationUser1>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("APPLICATION_USER");

            entity.Property(e => e.AccessConfig)
                .HasColumnName("ACCESS_CONFIG")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessEod)
                .HasColumnName("ACCESS_EOD")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessObi)
                .HasColumnName("ACCESS_OBI")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessOcis)
                .HasColumnName("ACCESS_OCIS")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessOcm)
                .HasColumnName("ACCESS_OCM")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessOcrm)
                .HasColumnName("ACCESS_OCRM")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessOrms)
                .HasColumnName("ACCESS_ORMS")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessOrs)
                .HasColumnName("ACCESS_ORS")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessOxi)
                .HasColumnName("ACCESS_OXI")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessOxihub)
                .HasColumnName("ACCESS_OXIHUB")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessPms)
                .HasColumnName("ACCESS_PMS")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessSc)
                .HasColumnName("ACCESS_SC")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessScbi)
                .HasColumnName("ACCESS_SCBI")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessSfa)
                .HasColumnName("ACCESS_SFA")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccessUtil)
                .HasColumnName("ACCESS_UTIL")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AccountLockedOutYn)
                .HasColumnName("ACCOUNT_LOCKED_OUT_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AppPassword)
                .IsRequired()
                .HasColumnName("APP_PASSWORD")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.AppUser)
                .IsRequired()
                .HasColumnName("APP_USER")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.AppUserDescription)
                .HasColumnName("APP_USER_DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.AppUserId)
                .HasColumnName("APP_USER_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.AppUserType)
                .IsRequired()
                .HasColumnName("APP_USER_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.AuthorizerYn)
                .HasColumnName("AUTHORIZER_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Comments)
                .HasColumnName("COMMENTS")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.DateHired)
                .HasColumnName("DATE_HIRED")
                .HasColumnType("DATE");

            entity.Property(e => e.DefCashierId)
                .HasColumnName("DEF_CASHIER_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.DefaultForm)
                .HasColumnName("DEFAULT_FORM")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.DefaultLanguage)
                .HasColumnName("DEFAULT_LANGUAGE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.DefaultResort)
                .HasColumnName("DEFAULT_RESORT")
                .IsUnicode(false);

            entity.Property(e => e.DefaultTerminal)
                .HasColumnName("DEFAULT_TERMINAL")
                .HasMaxLength(64)
                .IsUnicode(false);

            entity.Property(e => e.DeptId)
                .HasColumnName("DEPT_ID")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.DisabledUntil)
                .HasColumnName("DISABLED_UNTIL")
                .HasColumnType("DATE");

            entity.Property(e => e.EmpExtension)
                .HasColumnName("EMP_EXTENSION")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.EmpPager)
                .HasColumnName("EMP_PAGER")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.EmpStatus)
                .HasColumnName("EMP_STATUS")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.EmployeeNumber)
                .HasColumnName("EMPLOYEE_NUMBER")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ExpiresOn)
                .HasColumnName("EXPIRES_ON")
                .HasColumnType("DATE");

            entity.Property(e => e.ForcePasswordChangeYn)
                .HasColumnName("FORCE_PASSWORD_CHANGE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.FridayMax)
                .HasColumnName("FRIDAY_MAX")
                .HasColumnType("NUMBER");

            entity.Property(e => e.FridayMin)
                .HasColumnName("FRIDAY_MIN")
                .HasColumnType("NUMBER");

            entity.Property(e => e.GeneralFilepath)
                .HasColumnName("GENERAL_FILEPATH")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.GraceLogin)
                .HasColumnName("GRACE_LOGIN")
                .HasColumnType("NUMBER");

            entity.Property(e => e.HireType)
                .HasColumnName("HIRE_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.HourlyRate)
                .HasColumnName("HOURLY_RATE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.HoursPerWeek)
                .HasColumnName("HOURS_PER_WEEK")
                .HasColumnType("NUMBER");

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InactiveFrom)
                .HasColumnName("INACTIVE_FROM")
                .HasColumnType("DATE");

            entity.Property(e => e.InactiveReasonCode)
                .HasColumnName("INACTIVE_REASON_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InactiveTo)
                .HasColumnName("INACTIVE_TO")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.IsSuperuser)
                .HasColumnName("IS_SUPERUSER")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.LaptopId)
                .HasColumnName("LAPTOP_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.LastLoggedResort)
                .HasColumnName("LAST_LOGGED_RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.LastLoggedTimestamp)
                .HasColumnName("LAST_LOGGED_TIMESTAMP")
                .HasColumnType("DATE");

            entity.Property(e => e.LeadAddress)
                .HasColumnName("LEAD_ADDRESS")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.LeadAddressDet)
                .HasColumnName("LEAD_ADDRESS_DET")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.LeadComm)
                .HasColumnName("LEAD_COMM")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.LockoutDate)
                .HasColumnName("LOCKOUT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.LoginAttempts)
                .HasColumnName("LOGIN_ATTEMPTS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.LoginCro)
                .HasColumnName("LOGIN_CRO")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.MaleFemale)
                .HasColumnName("MALE_FEMALE")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.MaxCheckoutDays)
                .HasColumnName("MAX_CHECKOUT_DAYS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MaxDaysAfterCo)
                .HasColumnName("MAX_DAYS_AFTER_CO")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MaxUserSessions)
                .HasColumnName("MAX_USER_SESSIONS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MfnUserType)
                .HasColumnName("MFN_USER_TYPE")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.MondayMax)
                .HasColumnName("MONDAY_MAX")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MondayMin)
                .HasColumnName("MONDAY_MIN")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Name)
                .HasColumnName("NAME")
                .IsUnicode(false);

            entity.Property(e => e.OraclePassword)
                .HasColumnName("ORACLE_PASSWORD")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.OracleUid)
                .HasColumnName("ORACLE_UID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.OracleUser)
                .HasColumnName("ORACLE_USER")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.OtMultiplier)
                .HasColumnName("OT_MULTIPLIER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.PasswordChangeDays)
                .HasColumnName("PASSWORD_CHANGE_DAYS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.PasswordLastChange)
                .HasColumnName("PASSWORD_LAST_CHANGE")
                .HasColumnType("DATE");

            entity.Property(e => e.PasswordNextChange)
                .HasColumnName("PASSWORD_NEXT_CHANGE")
                .HasColumnType("DATE");

            entity.Property(e => e.PersonNameId)
                .HasColumnName("PERSON_NAME_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.PreventAccountLockout)
                .HasColumnName("PREVENT_ACCOUNT_LOCKOUT")
                .HasColumnType("CHAR(1)");

            entity.Property(e => e.PropertyAccessYn)
                .HasColumnName("PROPERTY_ACCESS_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.RateType)
                .HasColumnName("RATE_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.RehireYn)
                .HasColumnName("REHIRE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.SalaryInterval)
                .HasColumnName("SALARY_INTERVAL")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.SaturdayMax)
                .HasColumnName("SATURDAY_MAX")
                .HasColumnType("NUMBER");

            entity.Property(e => e.SaturdayMin)
                .HasColumnName("SATURDAY_MIN")
                .HasColumnType("NUMBER");

            entity.Property(e => e.SfaName)
                .HasColumnName("SFA_NAME")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.SundayMax)
                .HasColumnName("SUNDAY_MAX")
                .HasColumnType("NUMBER");

            entity.Property(e => e.SundayMin)
                .HasColumnName("SUNDAY_MIN")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TermReason)
                .HasColumnName("TERM_REASON")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.TerminatedDate)
                .HasColumnName("TERMINATED_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.ThursdayMax)
                .HasColumnName("THURSDAY_MAX")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ThursdayMin)
                .HasColumnName("THURSDAY_MIN")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Title)
                .HasColumnName("TITLE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.TuesdayMax)
                .HasColumnName("TUESDAY_MAX")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TuesdayMin)
                .HasColumnName("TUESDAY_MIN")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UserFilepath)
                .HasColumnName("USER_FILEPATH")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.UserGroupAdmin)
                .HasColumnName("USER_GROUP_ADMIN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.UserPbxId)
                .HasColumnName("USER_PBX_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.WednesdayMax)
                .HasColumnName("WEDNESDAY_MAX")
                .HasColumnType("NUMBER");

            entity.Property(e => e.WednesdayMin)
                .HasColumnName("WEDNESDAY_MIN")
                .HasColumnType("NUMBER");

            entity.Property(e => e.WeekMax)
                .HasColumnName("WEEK_MAX")
                .HasColumnType("NUMBER");

            entity.Property(e => e.WeekMin)
                .HasColumnName("WEEK_MIN")
                .HasColumnType("NUMBER");

            entity.Property(e => e.WeeklySalary)
                .HasColumnName("WEEKLY_SALARY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.WorkPermitExpdate)
                .HasColumnName("WORK_PERMIT_EXPDATE")
                .HasColumnType("DATE");

            entity.Property(e => e.WorkPermitNo)
                .HasColumnName("WORK_PERMIT_NO")
                .HasMaxLength(80)
                .IsUnicode(false);
        });
	}
}
