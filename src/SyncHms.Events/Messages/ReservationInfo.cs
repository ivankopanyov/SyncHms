namespace SyncHms.Events.Messages;

internal class ReservationInfo
{
    public decimal ReservationNumber { get; set; }

    public string Status { get; set; }

    public string? Room { get; set; }

    public DateTime? ArrivalDate { get; set; }

    public DateTime? DepartureDate { get; set; }

    public bool? NoPost {  get; set; }
}
