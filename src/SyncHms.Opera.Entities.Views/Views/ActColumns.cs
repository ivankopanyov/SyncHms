namespace SyncHms.Opera.Entities.Views;
	
public partial class ActColumns
{
    public string? ColumnName { get; set; }
    public string? DataType { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ActColumns>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("ACT_COLUMNS");

            entity.Property(e => e.ColumnName)
                .IsRequired()
                .HasColumnName("COLUMN_NAME")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.DataType)
                .HasColumnName("DATA_TYPE")
                .HasMaxLength(106)
                .IsUnicode(false);
        });
	}
}
