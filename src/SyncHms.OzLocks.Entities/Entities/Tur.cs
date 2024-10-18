namespace SyncHms.OzLocks.Entities;

public partial class Tur
{
    public int TurId { get; set; }

    public string TurName { get; set; } = null!;

    public string? TurCpname { get; set; }

    public string? TurCpphone { get; set; }

    public string? TurCpemail { get; set; }

    public string? TurAdress { get; set; }

    public string? TurComment { get; set; }

    public bool? Hidden { get; set; }

    public bool? CanDelete { get; set; }

    public int? TurUserId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string? CompanyName { get; set; }
}
