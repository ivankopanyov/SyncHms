namespace SyncHms.Opera.Entities.Views;
	
public partial class EventAttendeePreferences1
{
    public string? Resort { get; set; }
    public decimal? BookId { get; set; }
    public decimal? EventId { get; set; }
    public decimal? AttendeeProfileId { get; set; }
    public string? PreferenceType { get; set; }
    public string? Preference { get; set; }
    public string? PreferenceDesc { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<EventAttendeePreferences1>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("EVENT_ATTENDEE_PREFERENCES");

            entity.Property(e => e.AttendeeProfileId)
                .HasColumnName("ATTENDEE_PROFILE_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BookId)
                .HasColumnName("BOOK_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.EventId)
                .HasColumnName("EVENT_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Preference)
                .IsRequired()
                .HasColumnName("PREFERENCE")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.PreferenceDesc)
                .HasColumnName("PREFERENCE_DESC")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.PreferenceType)
                .IsRequired()
                .HasColumnName("PREFERENCE_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);
        });
	}
}
