namespace SyncHms.Opera.Entities.Tables;

public partial class SocialMediaSetup
{
    public decimal? SocialMediaPgId { get; set; }
    public string? SocialNetwork { get; set; }
    public string? PostToPage { get; set; }
    public string? PostToEmail { get; set; }
    public string? Resort { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? InactiveDate { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<SocialMediaSetup>(entity =>
        {
            entity.HasKey(e => new { e.SocialNetwork, e.PostToPage })
                .HasName("SOCMEDSET_PK");

            entity.ToTable("SOCIAL_MEDIA_SETUP");

            entity.HasIndex(e => e.SocialMediaPgId)
                .HasName("SOCIAL_MEDIA_SETUP_PK")
                .IsUnique();

            entity.HasIndex(e => new { e.Resort, e.SocialNetwork, e.PostToPage })
                .HasName("SOCIAL_MEDIA_SETUP_UK01")
                .IsUnique();

            entity.Property(e => e.SocialNetwork)
                .HasColumnName("SOCIAL_NETWORK")
                .HasMaxLength(200)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PostToPage)
                .HasColumnName("POST_TO_PAGE")
                .HasMaxLength(2000)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PostToEmail)
                .HasColumnName("POST_TO_EMAIL")
                .HasMaxLength(2000)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SocialMediaPgId)
                .HasColumnName("SOCIAL_MEDIA_PG_ID")
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
        });
	}
}
