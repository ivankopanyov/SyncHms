namespace SyncHms.Opera.Entities.Tables;

public partial class OrmsAlertsParameters
{
    public decimal? AlertId { get; set; }
    public string? AlertName { get; set; }
    public string? ParameterName { get; set; }
    public string? ParameterValue { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }

    public virtual OrmsAlertsDefinition Alert { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<OrmsAlertsParameters>(entity =>
        {
            entity.HasKey(e => new { e.AlertId, e.AlertName, e.ParameterName })
                .HasName("ORMS_ALERT_PARAMS_PK");

            entity.ToTable("ORMS_ALERTS_PARAMETERS");

            entity.Property(e => e.AlertId)
                .HasColumnName("ALERT_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.AlertName)
                .HasColumnName("ALERT_NAME")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ParameterName)
                .HasColumnName("PARAMETER_NAME")
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

            entity.Property(e => e.ParameterValue)
                .HasColumnName("PARAMETER_VALUE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

			if (!types.Contains(typeof(OrmsAlertsDefinition)))
				entity.Ignore(e => e.Alert);
			else
	            entity.HasOne(d => d.Alert)
	                .WithMany(p => p.OrmsAlertsParameters)
	                .HasForeignKey(d => d.AlertId)
	                .HasConstraintName("ORMS_ALERTS_PARAMETERS_FK");
        });
	}
}
