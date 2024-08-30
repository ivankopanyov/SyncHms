namespace SyncHms.Opera.Entities.Views;
	
public partial class MailLovResorts
{
    public string? Resort { get; set; }
    public string? Name { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<MailLovResorts>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("MAIL_LOV_RESORTS");

            entity.Property(e => e.Name)
                .HasColumnName("NAME")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);
        });
	}
}
