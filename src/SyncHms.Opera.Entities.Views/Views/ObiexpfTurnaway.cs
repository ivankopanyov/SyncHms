namespace SyncHms.Opera.Entities.Views;
	
public partial class ObiexpfTurnaway
{
    public string? RowKey { get; set; }
    public DateTime? RowTimestamp { get; set; }
    public string? Resort { get; set; }
    public string? AllCode { get; set; }
    public string? AllDesc { get; set; }
    public string? TurnawayType { get; set; }
    public string? TurnawayTypedesc { get; set; }
    public string? TurnawayCode { get; set; }
    public string? TurnawayDesc { get; set; }
    public string? ResortId { get; set; }
    public string? TurnawayTypeKey { get; set; }
    public string? TurnawayCodeKey { get; set; }
    public string? AllKey { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ObiexpfTurnaway>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("OBIEXPF_TURNAWAY");

            entity.Property(e => e.AllCode)
                .HasColumnName("ALL_CODE")
                .IsUnicode(false);

            entity.Property(e => e.AllDesc)
                .HasColumnName("ALL_DESC")
                .IsUnicode(false);

            entity.Property(e => e.AllKey)
                .HasColumnName("ALL_KEY")
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ResortId)
                .IsRequired()
                .HasColumnName("RESORT_ID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.RowKey)
                .HasColumnName("ROW_KEY")
                .HasMaxLength(41)
                .IsUnicode(false);

            entity.Property(e => e.RowTimestamp)
                .HasColumnName("ROW_TIMESTAMP")
                .HasColumnType("DATE");

            entity.Property(e => e.TurnawayCode)
                .HasColumnName("TURNAWAY_CODE")
                .IsUnicode(false);

            entity.Property(e => e.TurnawayCodeKey)
                .HasColumnName("TURNAWAY_CODE_KEY")
                .IsUnicode(false);

            entity.Property(e => e.TurnawayDesc)
                .HasColumnName("TURNAWAY_DESC")
                .IsUnicode(false);

            entity.Property(e => e.TurnawayType)
                .HasColumnName("TURNAWAY_TYPE")
                .IsUnicode(false);

            entity.Property(e => e.TurnawayTypeKey)
                .HasColumnName("TURNAWAY_TYPE_KEY")
                .IsUnicode(false);

            entity.Property(e => e.TurnawayTypedesc)
                .HasColumnName("TURNAWAY_TYPEDESC")
                .IsUnicode(false);
        });
	}
}
