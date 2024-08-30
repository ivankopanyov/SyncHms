namespace SyncHms.Opera.Entities.Tables;

public partial class MembershipClass
{
    public string? MembershipClass1 { get; set; }
    public string? Description { get; set; }
    public decimal? OrderBy { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? LoyaltyProgramYn { get; set; }
    public string? FrequentFlyerYn { get; set; }
    public string? ChainCode { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<MembershipClass>(entity =>
        {
            entity.HasKey(e => new { e.ChainCode, e.MembershipClass1 })
                .HasName("MEMBERSHIP_CLASS_PK");

            entity.ToTable("MEMBERSHIP_CLASS");

            entity.Property(e => e.ChainCode)
                .HasColumnName("CHAIN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.MembershipClass1)
                .HasColumnName("MEMBERSHIP_CLASS")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.FrequentFlyerYn)
                .HasColumnName("FREQUENT_FLYER_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql(@"'N'");

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.LoyaltyProgramYn)
                .HasColumnName("LOYALTY_PROGRAM_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql(@"'N'");

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");
        });
	}
}
