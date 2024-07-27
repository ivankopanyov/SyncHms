namespace SyncHms.Opera.Entities.Tables;

public partial class HealthCheckSubCategory
{
    public string? CategoryCode { get; set; }
    public string? Description { get; set; }
    public string? MainCategory { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public decimal? OrderBy { get; set; }

    public virtual HealthCheckMainCategory MainCategoryNavigation { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<HealthCheckSubCategory>(entity =>
        {
            entity.HasKey(e => new { e.MainCategory, e.CategoryCode })
                .HasName("HEALTH_CHECK_SUB_CAT_PK");

            entity.ToTable("HEALTH_CHECK_SUB_CATEGORY");

            entity.Property(e => e.MainCategory)
                .HasColumnName("MAIN_CATEGORY")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CategoryCode)
                .HasColumnName("CATEGORY_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(200)
                .IsUnicode(false);

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

			if (!types.Contains(typeof(HealthCheckMainCategory)))
				entity.Ignore(e => e.MainCategoryNavigation);
			else
	            entity.HasOne(d => d.MainCategoryNavigation)
	                .WithMany(p => p.HealthCheckSubCategory)
	                .HasForeignKey(d => d.MainCategory)
	                .OnDelete(DeleteBehavior.ClientSetNull)
	                .HasConstraintName("HEALTH_CHECK_SUB_CAT_FK");
        });
	}
}
