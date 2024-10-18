namespace SyncHms.OzLocks.Entities;

public partial class RoomPhoto
{
    public int Id { get; set; }

    public int? RoomId { get; set; }

    public int? TypeRoomId { get; set; }

    public byte[]? Photo { get; set; }

    public string? Com { get; set; }
}
