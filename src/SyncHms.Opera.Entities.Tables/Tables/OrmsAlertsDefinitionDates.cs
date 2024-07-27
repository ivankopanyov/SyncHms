namespace SyncHms.Opera.Entities.Tables;

public partial class OrmsAlertsDefinitionDates
{
    public decimal? AlertId { get; set; }
    public DateTime? AlertDate { get; set; }
    public string? DateFlag { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }

    public virtual OrmsAlertsDefinition Alert { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<OrmsAlertsDefinitionDates>(entity =>
        {
            entity.HasKey(e => new { e.AlertId, e.AlertDate })
                .HasName("ORMS_ALERTS_DEFINITION_DT_PK");

            entity.ToTable("ORMS_ALERTS_DEFINITION_DATES");

            entity.Property(e => e.AlertId)
                .HasColumnName("ALERT_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.AlertDate)
                .HasColumnName("ALERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.DateFlag)
                .HasColumnName("DATE_FLAG")
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql(@"'I'");

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

			if (!types.Contains(typeof(OrmsAlertsDefinition)))
				entity.Ignore(e => e.Alert);
			else
	            entity.HasOne(d => d.Alert)
	                .WithMany(p => p.OrmsAlertsDefinitionDates)
	                .HasForeignKey(d => d.AlertId)
	                .HasConstraintName("ORMS_ALERTS_DEFINITION_DT_FK");
        });
	}
}
