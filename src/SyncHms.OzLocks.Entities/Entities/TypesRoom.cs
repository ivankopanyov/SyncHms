namespace SyncHms.OzLocks.Entities;

public partial class TypesRoom
{
    public int TypeRoomId { get; set; }

    public string? TypeRoomName { get; set; }

    public int? TypeRoomBed { get; set; }

    public decimal TypeRoomPriceDay { get; set; }

    public decimal? TypeRoomPriceHour { get; set; }

    public decimal? TypeRoomPriceHourOv { get; set; }

    public decimal? TypeRoomDeposit { get; set; }

    public int? TypeRoomSize { get; set; }

    public int? TruserId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string? TypeRoomCom { get; set; }

    public bool? CanDelete { get; set; }

    public bool? Hidden { get; set; }

    public int? TimeCleaning { get; set; }

    public int? NumRooms { get; set; }
}
