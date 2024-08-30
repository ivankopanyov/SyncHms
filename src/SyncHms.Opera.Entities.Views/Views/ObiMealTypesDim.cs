namespace SyncHms.Opera.Entities.Views;
	
public partial class ObiMealTypesDim
{
    public string? AllCode { get; set; }
    public string? AllDesc { get; set; }
    public string? MealType { get; set; }
    public string? Description { get; set; }
    public decimal? OrderBy { get; set; }
    public DateTime? InactiveDate { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ObiMealTypesDim>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("OBI_MEAL_TYPES_DIM");

            entity.Property(e => e.AllCode)
                .HasColumnName("ALL_CODE")
                .IsUnicode(false);

            entity.Property(e => e.AllDesc)
                .HasColumnName("ALL_DESC")
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .IsUnicode(false);

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.MealType)
                .HasColumnName("MEAL_TYPE")
                .IsUnicode(false);

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");
        });
	}
}
