namespace SyncHms.Opera.Entities.Views;
	
public partial class MembershipEnrollmentCodes
{
    public string? EnrollmentCode { get; set; }
    public string? Description { get; set; }
    public decimal? OrderBy { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<MembershipEnrollmentCodes>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("MEMBERSHIP_ENROLLMENT_CODES");

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.EnrollmentCode)
                .IsRequired()
                .HasColumnName("ENROLLMENT_CODE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");
        });
	}
}
