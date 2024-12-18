namespace SyncHms.OzLocks.Entities;

public partial class Building
{
    public int? HotelId { get; set; }

    public int BuildingId { get; set; }

    public string? BuildingName { get; set; }

    public int? BuserId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string? BuildingComent { get; set; }

    public bool? CanDelete { get; set; }

    public bool? Hidden { get; set; }

    public int? BuildingNum { get; set; }
}
