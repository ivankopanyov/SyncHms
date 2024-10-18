namespace SyncHms.OzLocks.Entities;

public partial class Room
{
    public int? HotelId { get; set; }

    public int? BuildingId { get; set; }

    public int? FloorId { get; set; }

    public int RoomId { get; set; }

    public string? RoomName { get; set; }

    public int? RuserId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public int? RoomStateId { get; set; }

    public bool? CanDelete { get; set; }

    public int? RoomType { get; set; }

    public bool? Hidden { get; set; }

    public int? RoomEbed { get; set; }

    public string? RoomCom { get; set; }

    public decimal? RoomEbedPrice { get; set; }

    public int? RoomGuestCount { get; set; }

    public int? CurActionId { get; set; }

    public int? TurGrId { get; set; }

    public bool Checked { get; set; }

    public bool? Vip { get; set; }

    public bool? NotCleared { get; set; }

    public bool? Repair { get; set; }

    public bool? NotUsed { get; set; }

    public int? RoomNum { get; set; }

    public DateTime? FactTimeOut { get; set; }

    public string? FidelioId { get; set; }

    public int ShieldFlag { get; set; }
}
