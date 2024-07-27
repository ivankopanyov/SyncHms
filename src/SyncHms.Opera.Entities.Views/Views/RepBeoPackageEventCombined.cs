namespace SyncHms.Opera.Entities.Views;
	
public partial class RepBeoPackageEventCombined
{
    public string? Resort { get; set; }
    public decimal? BookId { get; set; }
    public decimal? EventIdNotCombined { get; set; }
    public decimal? EventLinkIdNotCombined { get; set; }
    public decimal? MasterEventIdNotCombined { get; set; }
    public decimal? CombinedEventId { get; set; }
    public decimal? Attendees { get; set; }
    public decimal? Guaranteed { get; set; }
    public decimal? SetAttendees { get; set; }
    public string? EvType { get; set; }
    public string? EvStatus { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Room { get; set; }
    public string? RoomSetup { get; set; }
    public decimal? SetupTime { get; set; }
    public decimal? SetdownTime { get; set; }
    public decimal? GroupId { get; set; }
    public DateTime? MinStartDateByPrintMethod { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<RepBeoPackageEventCombined>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("REP_BEO_PACKAGE_EVENT_COMBINED");

            entity.Property(e => e.Attendees)
                .HasColumnName("ATTENDEES")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BookId)
                .HasColumnName("BOOK_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CombinedEventId)
                .HasColumnName("COMBINED_EVENT_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.EndDate)
                .HasColumnName("END_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.EvStatus)
                .IsRequired()
                .HasColumnName("EV_STATUS")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.EvType)
                .IsRequired()
                .HasColumnName("EV_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.EventIdNotCombined)
                .HasColumnName("EVENT_ID_NOT_COMBINED")
                .HasColumnType("NUMBER");

            entity.Property(e => e.EventLinkIdNotCombined)
                .HasColumnName("EVENT_LINK_ID_NOT_COMBINED")
                .HasColumnType("NUMBER");

            entity.Property(e => e.GroupId)
                .HasColumnName("GROUP_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Guaranteed)
                .HasColumnName("GUARANTEED")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MasterEventIdNotCombined)
                .HasColumnName("MASTER_EVENT_ID_NOT_COMBINED")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MinStartDateByPrintMethod)
                .HasColumnName("MIN_START_DATE_BY_PRINT_METHOD")
                .HasColumnType("DATE");

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Room)
                .HasColumnName("ROOM")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.RoomSetup)
                .HasColumnName("ROOM_SETUP")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.SetAttendees)
                .HasColumnName("SET_ATTENDEES")
                .HasColumnType("NUMBER");

            entity.Property(e => e.SetdownTime)
                .HasColumnName("SETDOWN_TIME")
                .HasColumnType("NUMBER");

            entity.Property(e => e.SetupTime)
                .HasColumnName("SETUP_TIME")
                .HasColumnType("NUMBER");

            entity.Property(e => e.StartDate)
                .HasColumnName("START_DATE")
                .HasColumnType("DATE");
        });
	}
}
