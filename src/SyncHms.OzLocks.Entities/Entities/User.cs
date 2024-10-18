namespace SyncHms.OzLocks.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public int? UserPass { get; set; }

    public DateTime? UtimeStamp { get; set; }

    public int? UserIdE { get; set; }

    public bool? UcanDelete { get; set; }

    public bool? Uhidden { get; set; }

    public int? SigurId { get; set; }
}
