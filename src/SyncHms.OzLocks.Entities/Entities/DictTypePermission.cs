namespace SyncHms.OzLocks.Entities;

public partial class DictTypePermission
{
    public int TpermissionId { get; set; }

    public string? PermissionName { get; set; }

    public bool? Visible { get; set; }

    public string? Descr { get; set; }
}
