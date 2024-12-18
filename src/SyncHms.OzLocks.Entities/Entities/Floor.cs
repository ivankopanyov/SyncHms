namespace SyncHms.OzLocks.Entities;

public partial class Floor
{
    public int? BuildingId { get; set; }

    public int FloorId { get; set; }

    public string? FloorName { get; set; }

    public int? FuserId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string? FloorComent { get; set; }

    public bool? CanDelete { get; set; }

    public bool? Hidden { get; set; }

    public int? FloorNum { get; set; }
}
