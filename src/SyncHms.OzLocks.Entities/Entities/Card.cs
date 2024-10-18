namespace SyncHms.OzLocks.Entities;

public partial class Card
{
    public int CardId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public int? UserId { get; set; }

    public int? TypeOwner { get; set; }

    public int? Owner { get; set; }

    public int? TypeCard { get; set; }

    public string? SerialNo { get; set; }

    public int? RoomId { get; set; }

    public string? Uid { get; set; }

    public int? BuildingId { get; set; }

    public int? FloorId { get; set; }
}
