namespace SyncHms.Opera.Entities.Views;
	
public partial class ProductivityForecastView
{
    public decimal? NameId { get; set; }
    public string? ProdMonth { get; set; }
    public DateTime? FirstOfMonth { get; set; }
    public decimal? RoomNights { get; set; }
    public decimal? RoomRevenue { get; set; }
    public decimal? FoodRevenue { get; set; }
    public decimal? TotalRevenue { get; set; }
    public decimal? OtherRevenue { get; set; }
    public decimal? Persons { get; set; }
    public decimal? Adults { get; set; }
    public decimal? Children { get; set; }
    public decimal? Children1 { get; set; }
    public decimal? Children2 { get; set; }
    public decimal? Children3 { get; set; }
    public decimal? Children4 { get; set; }
    public decimal? Children5 { get; set; }
    public decimal? Arrivals { get; set; }
    public decimal? Cancels { get; set; }
    public decimal? NoShows { get; set; }
    public decimal? DayUse { get; set; }
    public decimal? AllotmentHeaderId { get; set; }
    public decimal? NonRevenue { get; set; }
    public decimal? ReservationNights { get; set; }
    public decimal? CancelledReservations { get; set; }
    public decimal? NoShowReservations { get; set; }
    public decimal? DayUseReservations { get; set; }
    public decimal? ReservationArrivals { get; set; }
    public decimal? ReservationNoOfStays { get; set; }
    public decimal? IndRoomNights { get; set; }
    public decimal? IndRoomRevenue { get; set; }
    public decimal? BlkRoomNights { get; set; }
    public decimal? BlkRoomRevenue { get; set; }
    public decimal? IndFoodRevenue { get; set; }
    public decimal? BlkFoodRevenue { get; set; }
    public decimal? IndOtherRevenue { get; set; }
    public decimal? BlkOtherRevenue { get; set; }
    public string? Resort { get; set; }
    public DateTime? ConsideredDate { get; set; }
    public string? BookingStatus { get; set; }
    public string? CurrencyCode { get; set; }
    public string? EventType { get; set; }
    public string? NameType { get; set; }
    public decimal? MasterNameId { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ProductivityForecastView>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("PRODUCTIVITY_FORECAST_VIEW");

            entity.Property(e => e.Adults)
                .HasColumnName("ADULTS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.AllotmentHeaderId)
                .HasColumnName("ALLOTMENT_HEADER_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Arrivals)
                .HasColumnName("ARRIVALS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BlkFoodRevenue)
                .HasColumnName("BLK_FOOD_REVENUE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BlkOtherRevenue)
                .HasColumnName("BLK_OTHER_REVENUE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BlkRoomNights)
                .HasColumnName("BLK_ROOM_NIGHTS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BlkRoomRevenue)
                .HasColumnName("BLK_ROOM_REVENUE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BookingStatus)
                .HasColumnName("BOOKING_STATUS")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CancelledReservations)
                .HasColumnName("CANCELLED_RESERVATIONS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Cancels)
                .HasColumnName("CANCELS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Children)
                .HasColumnName("CHILDREN")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Children1)
                .HasColumnName("CHILDREN1")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Children2)
                .HasColumnName("CHILDREN2")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Children3)
                .HasColumnName("CHILDREN3")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Children4)
                .HasColumnName("CHILDREN4")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Children5)
                .HasColumnName("CHILDREN5")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ConsideredDate)
                .HasColumnName("CONSIDERED_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.CurrencyCode)
                .HasColumnName("CURRENCY_CODE")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.DayUse)
                .HasColumnName("DAY_USE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.DayUseReservations)
                .HasColumnName("DAY_USE_RESERVATIONS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.EventType)
                .HasColumnName("EVENT_TYPE")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.FirstOfMonth)
                .HasColumnName("FIRST_OF_MONTH")
                .HasColumnType("DATE");

            entity.Property(e => e.FoodRevenue)
                .HasColumnName("FOOD_REVENUE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.IndFoodRevenue)
                .HasColumnName("IND_FOOD_REVENUE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.IndOtherRevenue)
                .HasColumnName("IND_OTHER_REVENUE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.IndRoomNights)
                .HasColumnName("IND_ROOM_NIGHTS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.IndRoomRevenue)
                .HasColumnName("IND_ROOM_REVENUE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MasterNameId)
                .HasColumnName("MASTER_NAME_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.NameId)
                .HasColumnName("NAME_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.NameType)
                .HasColumnName("NAME_TYPE")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.NoShowReservations)
                .HasColumnName("NO_SHOW_RESERVATIONS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.NoShows)
                .HasColumnName("NO_SHOWS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.NonRevenue)
                .HasColumnName("NON_REVENUE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.OtherRevenue)
                .HasColumnName("OTHER_REVENUE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Persons)
                .HasColumnName("PERSONS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ProdMonth)
                .HasColumnName("PROD_MONTH")
                .HasMaxLength(6)
                .IsUnicode(false);

            entity.Property(e => e.ReservationArrivals)
                .HasColumnName("RESERVATION_ARRIVALS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ReservationNights)
                .HasColumnName("RESERVATION_NIGHTS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ReservationNoOfStays)
                .HasColumnName("RESERVATION_NO_OF_STAYS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.RoomNights)
                .HasColumnName("ROOM_NIGHTS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.RoomRevenue)
                .HasColumnName("ROOM_REVENUE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TotalRevenue)
                .HasColumnName("TOTAL_REVENUE")
                .HasColumnType("NUMBER");
        });
	}
}
