namespace SyncHms.Opera.Entities.Tables;

public partial class ResortTransportation
{
    public string? Resort { get; set; }
    public string? TransportCode { get; set; }
    public string? Phone { get; set; }
    public decimal? Distance { get; set; }
    public string? DistanceType { get; set; }
    public string? Description { get; set; }
    public string? Comments { get; set; }
    public decimal? OrderBy { get; set; }
    public string? PriceRange { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? Label { get; set; }
    public string? Website { get; set; }

    public virtual Resort ResortNavigation { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ResortTransportation>(entity =>
        {
            entity.HasKey(e => new { e.Resort, e.TransportCode })
                .HasName("RT_PK");

            entity.ToTable("RESORT_TRANSPORTATION");

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.TransportCode)
                .HasColumnName("TRANSPORT_CODE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.Comments)
                .HasColumnName("COMMENTS")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.Property(e => e.Distance)
                .HasColumnName("DISTANCE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.DistanceType)
                .HasColumnName("DISTANCE_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Label)
                .HasColumnName("LABEL")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Phone)
                .HasColumnName("PHONE")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.PriceRange)
                .HasColumnName("PRICE_RANGE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Website)
                .HasColumnName("WEBSITE")
                .HasMaxLength(2000)
                .IsUnicode(false);

			if (!types.Contains(typeof(Resort)))
				entity.Ignore(e => e.ResortNavigation);
			else
	            entity.HasOne(d => d.ResortNavigation)
	                .WithMany(p => p.ResortTransportation)
	                .HasForeignKey(d => d.Resort)
	                .OnDelete(DeleteBehavior.ClientSetNull)
	                .HasConstraintName("RT_RESORT_FK");
        });
	}
}
