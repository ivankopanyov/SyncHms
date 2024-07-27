namespace SyncHms.Opera.Entities.Tables;

public partial class ClaimAdjLimitsResorts
{
    public string? ClaimAdjLimitCode { get; set; }
    public string? Resort { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ClaimAdjLimitsResorts>(entity =>
        {
            entity.HasKey(e => new { e.ClaimAdjLimitCode, e.Resort })
                .HasName("CLAIM_ADJ_LIMITS_RESORTS_PK");

            entity.ToTable("CLAIM_ADJ_LIMITS_RESORTS");

            entity.Property(e => e.ClaimAdjLimitCode)
                .HasColumnName("CLAIM_ADJ_LIMIT_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
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
