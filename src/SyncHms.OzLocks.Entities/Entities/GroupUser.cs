namespace SyncHms.OzLocks.Entities;

public partial class GroupUser
{
    public int GroupId { get; set; }

    public string? GroupName { get; set; }

    public bool? CanDelete { get; set; }

    public bool? Hidden { get; set; }
}
