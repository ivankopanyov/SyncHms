namespace SyncHms.Opera.Entities.Tables;

public partial class MembershipRateGroupCodes
{
    public string? MemRateGroup { get; set; }
    public string? RateCode { get; set; }
    public DateTime? InactiveDate { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? ChainCode { get; set; }

    public virtual MembershipRateGroups MembershipRateGroups { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<MembershipRateGroupCodes>(entity =>
        {
            entity.HasKey(e => new { e.ChainCode, e.MemRateGroup, e.RateCode })
                .HasName("MRGM_PK");

            entity.ToTable("MEMBERSHIP_RATE_GROUP_CODES");

            entity.Property(e => e.ChainCode)
                .HasColumnName("CHAIN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.MemRateGroup)
                .HasColumnName("MEM_RATE_GROUP")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.RateCode)
                .HasColumnName("RATE_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

			if (!types.Contains(typeof(MembershipRateGroups)))
				entity.Ignore(e => e.MembershipRateGroups);
			else
	            entity.HasOne(d => d.MembershipRateGroups)
	                .WithMany(p => p.MembershipRateGroupCodes)
	                .HasForeignKey(d => new { d.ChainCode, d.MemRateGroup })
	                .OnDelete(DeleteBehavior.ClientSetNull)
	                .HasConstraintName("MEM_RATE_GROUPS_FK");
        });
	}
}
