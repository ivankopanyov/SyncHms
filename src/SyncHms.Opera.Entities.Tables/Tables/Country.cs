namespace SyncHms.Opera.Entities.Tables;

public partial class Country
{
    public Country()
    {
        CountryLanguages = new HashSet<CountryLanguages>();
        Resort = new HashSet<Resort>();
        ResortCountry = new HashSet<ResortCountry>();
    }

    public string? Name { get; set; }
    public string? Status { get; set; }
    public string? CountryCode { get; set; }
    public string? GuestAddressFormat { get; set; }
    public string? PropertyAddressFormat { get; set; }
    public string? CountryName { get; set; }
    public string? StatisticCode { get; set; }
    public string? RegionCode { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public DateTime? InactiveDate { get; set; }
    public string? IsoCode { get; set; }
    public string? IsoName { get; set; }
    public decimal? ShowSequence { get; set; }
    public decimal? PrintSequence { get; set; }
    public string? CountryMainGroup { get; set; }
    public string? CanDeleteYn { get; set; }
    public string? AddressdoctorMode { get; set; }
    public string? DisplayCountryFlagYn { get; set; }

    public virtual ICollection<CountryLanguages> CountryLanguages { get; set; }
    public virtual ICollection<Resort> Resort { get; set; }
    public virtual ICollection<ResortCountry> ResortCountry { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryCode)
                .HasName("COUNTRY_PK");

            entity.ToTable("COUNTRY$");

            entity.Property(e => e.CountryCode)
                .HasColumnName("COUNTRY_CODE")
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.Property(e => e.AddressdoctorMode)
                .HasColumnName("ADDRESSDOCTOR_MODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CanDeleteYn)
                .HasColumnName("CAN_DELETE_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'Y'");

            entity.Property(e => e.CountryMainGroup)
                .HasColumnName("COUNTRY_MAIN_GROUP")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.CountryName)
                .IsRequired()
                .HasColumnName("COUNTRY_NAME")
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.Property(e => e.DisplayCountryFlagYn)
                .HasColumnName("DISPLAY_COUNTRY_FLAG_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.GuestAddressFormat)
                .IsRequired()
                .HasColumnName("GUEST_ADDRESS_FORMAT")
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("'DEFAULT_GUEST'");

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.IsoCode)
                .HasColumnName("ISO_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.IsoName)
                .HasColumnName("ISO_NAME")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .HasColumnName("NAME")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.PrintSequence)
                .HasColumnName("PRINT_SEQUENCE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.PropertyAddressFormat)
                .IsRequired()
                .HasColumnName("PROPERTY_ADDRESS_FORMAT")
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("'DEFAULT_PROPERTY'");

            entity.Property(e => e.RegionCode)
                .HasColumnName("REGION_CODE")
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.Property(e => e.ShowSequence)
                .HasColumnName("SHOW_SEQUENCE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.StatisticCode)
                .IsRequired()
                .HasColumnName("STATISTIC_CODE")
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.Property(e => e.Status)
                .HasColumnName("STATUS")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");
        
			if (!types.Contains(typeof(CountryLanguages)))
				entity.Ignore(e => e.CountryLanguages);

			if (!types.Contains(typeof(Resort)))
				entity.Ignore(e => e.Resort);

			if (!types.Contains(typeof(ResortCountry)))
				entity.Ignore(e => e.ResortCountry);
		});
	}
}
