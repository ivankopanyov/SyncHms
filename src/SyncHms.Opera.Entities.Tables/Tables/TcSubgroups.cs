namespace SyncHms.Opera.Entities.Tables;

public partial class TcSubgroups
{
    public TcSubgroups()
    {
        TrxCodes = new HashSet<TrxCodes>();
        TrxRoutingInstructions = new HashSet<TrxRoutingInstructions>();
    }

    public string? Resort { get; set; }
    public string? TcGroup { get; set; }
    public string? TcSubgroup { get; set; }
    public string? Description { get; set; }
    public string? TcTransactionType { get; set; }
    public string? Class1MandatoryYn { get; set; }
    public string? Class2MandatoryYn { get; set; }
    public string? TaxYn { get; set; }
    public string? ResultIncludedInSumArray { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? IndRevenueGp { get; set; }
    public string? SubGpType { get; set; }
    public string? InternalYn { get; set; }
    public decimal? OrderBy { get; set; }
    public string? FrequentFlyerYn { get; set; }
    public string? GpPointsRedemptionYn { get; set; }

    public virtual TcGroups TcGroups { get; set; }
    public virtual ICollection<TrxCodes> TrxCodes { get; set; }
    public virtual ICollection<TrxRoutingInstructions> TrxRoutingInstructions { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<TcSubgroups>(entity =>
        {
            entity.HasKey(e => new { e.Resort, e.TcSubgroup, e.TcGroup })
                .HasName("TC$_SUBGROUPS_PK");

            entity.ToTable("TC$_SUBGROUPS");

            entity.HasIndex(e => e.TcGroup)
                .HasName("TG_TMG_FK_I");

            entity.HasIndex(e => new { e.TcGroup, e.Resort })
                .HasName("TG_TMG_FK_IDX");

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.TcSubgroup)
                .HasColumnName("TC_SUBGROUP")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.TcGroup)
                .HasColumnName("TC_GROUP")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Class1MandatoryYn)
                .HasColumnName("CLASS_1_MANDATORY_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Class2MandatoryYn)
                .HasColumnName("CLASS_2_MANDATORY_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.FrequentFlyerYn)
                .HasColumnName("FREQUENT_FLYER_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.GpPointsRedemptionYn)
                .HasColumnName("GP_POINTS_REDEMPTION_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.IndRevenueGp)
                .HasColumnName("IND_REVENUE_GP")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.InternalYn)
                .HasColumnName("INTERNAL_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ResultIncludedInSumArray)
                .HasColumnName("RESULT_INCLUDED_IN_SUM_ARRAY")
                .HasMaxLength(5)
                .IsUnicode(false);

            entity.Property(e => e.SubGpType)
                .HasColumnName("SUB_GP_TYPE")
                .HasMaxLength(2)
                .IsUnicode(false);

            entity.Property(e => e.TaxYn)
                .HasColumnName("TAX_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.TcTransactionType)
                .IsRequired()
                .HasColumnName("TC_TRANSACTION_TYPE")
                .HasColumnType("CHAR(5)");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

			if (!types.Contains(typeof(TcGroups)))
				entity.Ignore(e => e.TcGroups);
			else
	            entity.HasOne(d => d.TcGroups)
	                .WithMany(p => p.TcSubgroups)
	                .HasForeignKey(d => new { d.TcGroup, d.Resort })
	                .OnDelete(DeleteBehavior.ClientSetNull)
	                .HasConstraintName("TG_TMG_FK");
        
			if (!types.Contains(typeof(TrxCodes)))
				entity.Ignore(e => e.TrxCodes);

			if (!types.Contains(typeof(TrxRoutingInstructions)))
				entity.Ignore(e => e.TrxRoutingInstructions);
		});
	}
}
