namespace SyncHms.OzLocks.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? RoomId { get; set; }

    public DateTime? TimeIn { get; set; }

    public DateTime? TimeOut { get; set; }

    public int? TurGrId { get; set; }

    public int? GuestId { get; set; }

    public int? BuserId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public bool? Used { get; set; }

    public string? Com { get; set; }

    public decimal? Prepayment { get; set; }
}
