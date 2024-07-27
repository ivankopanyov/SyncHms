namespace SyncHms.Opera.Entities.Views;
	
public partial class LookupTypes
{
    public string? Code { get; set; }
    public string? Description { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<LookupTypes>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("LOOKUP_TYPES");

            entity.Property(e => e.Code)
                .HasColumnName("CODE")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .IsUnicode(false);
        });
	}
}
