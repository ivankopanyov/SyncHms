namespace SyncHms.Opera.Entities.Tables;

public partial class ReservationCommPref
{
    public string? Resort { get; set; }
    public decimal? ResvNameId { get; set; }
    public decimal? PhoneId { get; set; }
    public decimal? TopicId { get; set; }
    public string? OptInYn { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }

    public virtual NamePhone Phone { get; set; }
    public virtual ReservationName Res { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ReservationCommPref>(entity =>
        {
            entity.HasKey(e => new { e.Resort, e.ResvNameId, e.PhoneId, e.TopicId })
                .HasName("RESERVATION_COMM_PREF_PK");

            entity.ToTable("RESERVATION_COMM_PREF");

            entity.HasIndex(e => e.PhoneId)
                .HasName("RESV_COMM_PREF_PHONE_FKI");

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ResvNameId)
                .HasColumnName("RESV_NAME_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.PhoneId)
                .HasColumnName("PHONE_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TopicId)
                .HasColumnName("TOPIC_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.OptInYn)
                .IsRequired()
                .HasColumnName("OPT_IN_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

			if (!types.Contains(typeof(NamePhone)))
				entity.Ignore(e => e.Phone);
			else
	            entity.HasOne(d => d.Phone)
	                .WithMany(p => p.ReservationCommPref)
	                .HasForeignKey(d => d.PhoneId)
	                .HasConstraintName("RESV_COMM_PREF_PHONE_FK");

			if (!types.Contains(typeof(ReservationName)))
				entity.Ignore(e => e.Res);
			else
	            entity.HasOne(d => d.Res)
	                .WithMany(p => p.ReservationCommPref)
	                .HasForeignKey(d => new { d.Resort, d.ResvNameId })
	                .OnDelete(DeleteBehavior.ClientSetNull)
	                .HasConstraintName("RESV_COMM_PREF_RESV_FK");
        });
	}
}
