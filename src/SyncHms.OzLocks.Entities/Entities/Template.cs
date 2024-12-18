namespace SyncHms.OzLocks.Entities;

public partial class Template
{
    public int Id { get; set; }

    public int? GroupId { get; set; }

    public string? TemlateName { get; set; }

    public byte[]? Rtf { get; set; }

    public int? UserId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public bool? IsInternal { get; set; }
}
