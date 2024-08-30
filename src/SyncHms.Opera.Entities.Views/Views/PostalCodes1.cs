namespace SyncHms.Opera.Entities.Views;
	
public partial class PostalCodes1
{
    public string? CountryCode { get; set; }
    public string? StateCode { get; set; }
    public string? Description { get; set; }
    public string? RegionCode { get; set; }
    public string? PostalCodeFrom { get; set; }
    public string? PostalCodeTo { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? FiscalRegionCode { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Scity { get; set; }
    public string? ChainCode { get; set; }
    public decimal? Seq { get; set; }
    public string? TerritoryCode { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<PostalCodes1>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("POSTAL_CODES");

            entity.Property(e => e.ChainCode)
                .HasColumnName("CHAIN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.City)
                .HasColumnName("CITY")
                .HasMaxLength(40)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CountryCode)
                .HasColumnName("COUNTRY_CODE")
                .HasMaxLength(3)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(300)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.District)
                .HasColumnName("DISTRICT")
                .HasMaxLength(10)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.FiscalRegionCode)
                .HasColumnName("FISCAL_REGION_CODE")
                .HasMaxLength(40)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PostalCodeFrom)
                .HasColumnName("POSTAL_CODE_FROM")
                .HasMaxLength(15)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PostalCodeTo)
                .HasColumnName("POSTAL_CODE_TO")
                .HasMaxLength(15)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RegionCode)
                .HasColumnName("REGION_CODE")
                .HasMaxLength(3)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Scity)
                .HasColumnName("SCITY")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.Seq)
                .HasColumnName("SEQ")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.StateCode)
                .HasColumnName("STATE_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.TerritoryCode)
                .HasColumnName("TERRITORY_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");
        });
	}
}
