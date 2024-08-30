namespace SyncHms.Opera.Entities.Tables;

public partial class TurnawayTemplate
{
    public string? TurnawayCode { get; set; }
    public string? Description { get; set; }
    public DateTime? InactiveDate { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public decimal? SellSequence { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? TurnawayType { get; set; }
    public string? CanDeleteYn { get; set; }
    public string? ChainCode { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<TurnawayTemplate>(entity =>
        {
            entity.HasKey(e => new { e.ChainCode, e.TurnawayCode })
                .HasName("TURNAWAY_TEMPLATE_PK");

            entity.ToTable("TURNAWAY_TEMPLATE");

            entity.Property(e => e.ChainCode)
                .HasColumnName("CHAIN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.TurnawayCode)
                .HasColumnName("TURNAWAY_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CanDeleteYn)
                .HasColumnName("CAN_DELETE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(200)
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

            entity.Property(e => e.SellSequence)
                .HasColumnName("SELL_SEQUENCE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TurnawayType)
                .HasColumnName("TURNAWAY_TYPE")
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
