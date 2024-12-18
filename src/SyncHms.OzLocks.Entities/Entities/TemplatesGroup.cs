namespace SyncHms.OzLocks.Entities;

public partial class TemplatesGroup
{
    public int Id { get; set; }

    public string? GroupName { get; set; }

    public int? UserId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public bool? IsInternal { get; set; }
}
