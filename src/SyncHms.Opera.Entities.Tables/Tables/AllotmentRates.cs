namespace SyncHms.Opera.Entities.Tables;

public partial class AllotmentRates
{
    public string? Resort { get; set; }
    public decimal? AllotmentHeaderId { get; set; }
    public string? RateCode { get; set; }
    public string? PrimaryYn { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public string? RecordType { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? OffsetType { get; set; }

    public virtual AllotmentHeader AllotmentHeader { get; set; }
    public virtual AllotmentOffsetType AllotmentOffsetType { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<AllotmentRates>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("ALLOTMENT$RATES");

            entity.HasIndex(e => new { e.AllotmentHeaderId, e.Resort, e.OffsetType })
                .HasName("ARATES_AOFFSET_FKI");

            entity.HasIndex(e => new { e.Resort, e.AllotmentHeaderId, e.RecordType, e.RateCode })
                .HasName("ALLOTMENT_RATES_PK")
                .IsUnique();

            entity.Property(e => e.AllotmentHeaderId)
                .HasColumnName("ALLOTMENT_HEADER_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BeginDate)
                .HasColumnName("BEGIN_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.EndDate)
                .HasColumnName("END_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.OffsetType)
                .IsRequired()
                .HasColumnName("OFFSET_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PrimaryYn)
                .HasColumnName("PRIMARY_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RateCode)
                .IsRequired()
                .HasColumnName("RATE_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RecordType)
                .IsRequired()
                .HasColumnName("RECORD_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql(@"'ALLOTMENT'");

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

			if (!types.Contains(typeof(AllotmentHeader)))
				entity.Ignore(e => e.AllotmentHeader);
			else
	            entity.HasOne(d => d.AllotmentHeader)
	                .WithMany()
	                .HasForeignKey(d => new { d.AllotmentHeaderId, d.Resort })
	                .HasConstraintName("ARATES_AH_FK");

			if (!types.Contains(typeof(AllotmentOffsetType)))
				entity.Ignore(e => e.AllotmentOffsetType);
			else
	            entity.HasOne(d => d.AllotmentOffsetType)
	                .WithMany()
	                .HasForeignKey(d => new { d.AllotmentHeaderId, d.Resort, d.OffsetType })
	                .HasConstraintName("ARATES_AOFFSET_FK");
        });
	}
}
