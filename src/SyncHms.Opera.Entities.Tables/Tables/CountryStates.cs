namespace SyncHms.Opera.Entities.Tables;

public partial class CountryStates
{
    public decimal? StateId { get; set; }
    public string? CountryCode { get; set; }
    public string? StateCode { get; set; }
    public string? Description { get; set; }
    public decimal? OrderBy { get; set; }
    public DateTime? InactiveDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? ChainCode { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<CountryStates>(entity =>
        {
            entity.HasKey(e => new { e.ChainCode, e.StateId })
                .HasName("CST_PK");

            entity.ToTable("COUNTRY_STATES");

            entity.HasIndex(e => new { e.CountryCode, e.StateCode, e.ChainCode })
                .HasName("CST_COUNTRY_UK")
                .IsUnique();

            entity.Property(e => e.ChainCode)
                .HasColumnName("CHAIN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.StateId)
                .HasColumnName("STATE_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CountryCode)
                .IsRequired()
                .HasColumnName("COUNTRY_CODE")
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(80)
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

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.StateCode)
                .IsRequired()
                .HasColumnName("STATE_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");
        });
	}
}
