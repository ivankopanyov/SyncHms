namespace SyncHms.Opera.Entities.Views;
	
public partial class EmployeeAction
{
    public decimal? ActionId { get; set; }
    public string? ActionType { get; set; }
    public string? ActionDescription { get; set; }
    public string? Machine { get; set; }
    public decimal? PermissionAppUserId { get; set; }
    public string? Terminal { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public string? ModuleType { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<EmployeeAction>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("EMPLOYEE_ACTION");

            entity.Property(e => e.ActionDescription)
                .HasColumnName("ACTION_DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.ActionId)
                .HasColumnName("ACTION_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ActionType)
                .HasColumnName("ACTION_TYPE")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Machine)
                .HasColumnName("MACHINE")
                .HasMaxLength(64)
                .IsUnicode(false);

            entity.Property(e => e.ModuleType)
                .HasColumnName("MODULE_TYPE")
                .HasColumnType("CHAR(3)");

            entity.Property(e => e.PermissionAppUserId)
                .HasColumnName("PERMISSION_APP_USER_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Terminal)
                .HasColumnName("TERMINAL")
                .HasMaxLength(100)
                .IsUnicode(false);
        });
	}
}
