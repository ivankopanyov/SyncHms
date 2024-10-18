namespace SyncHms.OzLocks.Entities;

public partial class TurGr
{
    public int TurGrId { get; set; }

    public string? TurGrName { get; set; }

    public int? TurGrTur { get; set; }

    public string? TurGrGidName { get; set; }

    public string? TurGrGidPhone { get; set; }

    public string? TurGrGidEmail { get; set; }

    public int? TurGrGidTypeDoc { get; set; }

    public string? TurGrGidDocS { get; set; }

    public string? TurGrGidDocNo { get; set; }

    public int? TurGrGuestCount { get; set; }

    public string? TurGrComment { get; set; }

    public bool? Hidden { get; set; }

    public bool? CanDelete { get; set; }

    public int? TgrUserId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public int? TurGrGuestIn { get; set; }

    public bool? Booked { get; set; }

    public DateTime? TimeIn { get; set; }

    public DateTime? TimeOut { get; set; }

    public bool? Out { get; set; }
}
