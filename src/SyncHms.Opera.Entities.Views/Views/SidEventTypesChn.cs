namespace SyncHms.Opera.Entities.Views;
	
public partial class SidEventTypesChn
{
    public string? EventType { get; set; }
    public string? Description { get; set; }
    public byte? OrderBy { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? InactiveDate { get; set; }
    public string? MealType { get; set; }
    public string? ChainCode { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<SidEventTypesChn>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("SID_EVENT$TYPES_CHN");

            entity.Property(e => e.ChainCode)
                .IsRequired()
                .HasColumnName("CHAIN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.EndTime)
                .HasColumnName("END_TIME")
                .HasColumnType("DATE");

            entity.Property(e => e.EventType)
                .IsRequired()
                .HasColumnName("EVENT_TYPE")
                .HasMaxLength(20)
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

            entity.Property(e => e.MealType)
                .HasColumnName("MEAL_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER(2)");

            entity.Property(e => e.StartTime)
                .HasColumnName("START_TIME")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");
        });
	}
}
