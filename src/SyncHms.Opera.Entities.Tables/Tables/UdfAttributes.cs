namespace SyncHms.Opera.Entities.Tables;

public partial class UdfAttributes
{
    public string? TableName { get; set; }
    public string? UdfType { get; set; }
    public string? UdfAttribute { get; set; }
    public string? Description { get; set; }
    public DateTime? InactiveDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? UdfCategory { get; set; }
    public decimal? OrderBy { get; set; }

    public virtual UdfTypes UdfTypes { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<UdfAttributes>(entity =>
        {
            entity.HasKey(e => new { e.TableName, e.UdfCategory, e.UdfType, e.UdfAttribute })
                .HasName("UDF_ATTRIBUTES_PK");

            entity.ToTable("UDF_ATTRIBUTES");

            entity.Property(e => e.TableName)
                .HasColumnName("TABLE_NAME")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.UdfCategory)
                .HasColumnName("UDF_CATEGORY")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.UdfType)
                .HasColumnName("UDF_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.UdfAttribute)
                .HasColumnName("UDF_ATTRIBUTE")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(2000)
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

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

			if (!types.Contains(typeof(UdfTypes)))
				entity.Ignore(e => e.UdfTypes);
			else
	            entity.HasOne(d => d.UdfTypes)
	                .WithMany(p => p.UdfAttributes)
	                .HasForeignKey(d => new { d.TableName, d.UdfCategory, d.UdfType })
	                .OnDelete(DeleteBehavior.ClientSetNull)
	                .HasConstraintName("UDF_ATTRIBUTES_FK1");
        });
	}
}
