namespace SyncHms.Opera.Entities.Tables;

public partial class OwsGuestLoginPassword
{
    public string? LoginName { get; set; }
    public string? Password { get; set; }
    public decimal? PwdChgNo { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public string? ChainCode { get; set; }
    public string? Resort { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<OwsGuestLoginPassword>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("OWS_GUEST_LOGIN_PASSWORD");

            entity.HasIndex(e => new { e.LoginName, e.Resort, e.ChainCode })
                .HasName("OWS_GUEST_LOGIN_PASSWORD_INDX1");

            entity.Property(e => e.ChainCode)
                .IsRequired()
                .HasColumnName("CHAIN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.LoginName)
                .IsRequired()
                .HasColumnName("LOGIN_NAME")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.Password)
                .IsRequired()
                .HasColumnName("PASSWORD")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.PwdChgNo)
                .HasColumnName("PWD_CHG_NO")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);
        });
	}
}
