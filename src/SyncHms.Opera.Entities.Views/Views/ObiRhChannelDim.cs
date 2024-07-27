namespace SyncHms.Opera.Entities.Views;
	
public partial class ObiRhChannelDim
{
    public string? AllCode { get; set; }
    public string? AllDesc { get; set; }
    public string? ChannelCode { get; set; }
    public string? ChannelDesc { get; set; }
    public decimal? OrderBy { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ObiRhChannelDim>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("OBI_RH_CHANNEL_DIM");

            entity.Property(e => e.AllCode)
                .HasColumnName("ALL_CODE")
                .IsUnicode(false);

            entity.Property(e => e.AllDesc)
                .HasColumnName("ALL_DESC")
                .IsUnicode(false);

            entity.Property(e => e.ChannelCode)
                .HasColumnName("CHANNEL_CODE")
                .IsUnicode(false);

            entity.Property(e => e.ChannelDesc)
                .HasColumnName("CHANNEL_DESC")
                .IsUnicode(false);

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");
        });
	}
}
