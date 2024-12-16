namespace SyncHms.OzLocks.Entities;

public partial class AuditUser
{
    public int Id { get; set; }

    public int? CatId { get; set; }

    public int? ActId { get; set; }

    public DateTime? TimeStamp { get; set; } = DateTime.Now;

    public int? UserId { get; set; }

    public int? ObjId { get; set; }
}
