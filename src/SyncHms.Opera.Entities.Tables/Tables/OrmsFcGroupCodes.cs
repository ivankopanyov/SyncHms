namespace SyncHms.Opera.Entities.Tables;

public partial class OrmsFcGroupCodes
{
    public OrmsFcGroupCodes()
    {
        OrmsFcGroupDtls = new HashSet<OrmsFcGroupDtls>();
    }

    public string? Resort { get; set; }
    public string? CodeType { get; set; }
    public string? GroupCode { get; set; }
    public string? GroupDesc { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }

    public virtual ICollection<OrmsFcGroupDtls> OrmsFcGroupDtls { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<OrmsFcGroupCodes>(entity =>
        {
            entity.HasKey(e => new { e.Resort, e.CodeType, e.GroupCode })
                .HasName("ORMS_FC_GRP_CODES_PK");

            entity.ToTable("ORMS_FC_GROUP_CODES");

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CodeType)
                .HasColumnName("CODE_TYPE")
                .HasMaxLength(2)
                .IsUnicode(false);

            entity.Property(e => e.GroupCode)
                .HasColumnName("GROUP_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.GroupDesc)
                .IsRequired()
                .HasColumnName("GROUP_DESC")
                .HasMaxLength(80)
                .IsUnicode(false);

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
        
			if (!types.Contains(typeof(OrmsFcGroupDtls)))
				entity.Ignore(e => e.OrmsFcGroupDtls);
		});
	}
}
