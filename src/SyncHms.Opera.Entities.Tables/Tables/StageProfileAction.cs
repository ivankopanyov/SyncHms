namespace SyncHms.Opera.Entities.Tables;

public partial class StageProfileAction
{
    public decimal? ActionId { get; set; }
    public string? ActionType { get; set; }
    public string? ActionDesc { get; set; }
    public decimal? ProfileId { get; set; }
    public string? Resort { get; set; }
    public string? InterfaceId { get; set; }
    public string? ChainCode { get; set; }
    public string? Module { get; set; }
    public string? Machine { get; set; }
    public string? Terminal { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<StageProfileAction>(entity =>
        {
            entity.HasKey(e => e.ActionId)
                .HasName("STAGE_PROFILE_ACTION_PK");

            entity.ToTable("STAGE_PROFILE_ACTION");

            entity.HasIndex(e => new { e.Resort, e.ProfileId })
                .HasName("STG_PROFILE_ACTION_ID_IDX");

            entity.Property(e => e.ActionId)
                .HasColumnName("ACTION_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ActionDesc)
                .IsRequired()
                .HasColumnName("ACTION_DESC")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.ActionType)
                .IsRequired()
                .HasColumnName("ACTION_TYPE")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ChainCode)
                .HasColumnName("CHAIN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InterfaceId)
                .IsRequired()
                .HasColumnName("INTERFACE_ID")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Machine)
                .HasColumnName("MACHINE")
                .HasMaxLength(64)
                .IsUnicode(false);

            entity.Property(e => e.Module)
                .HasColumnName("MODULE")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.ProfileId)
                .HasColumnName("PROFILE_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Terminal)
                .HasColumnName("TERMINAL")
                .HasMaxLength(100)
                .IsUnicode(false);
        });
	}
}
