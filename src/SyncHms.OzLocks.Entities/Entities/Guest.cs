namespace SyncHms.OzLocks.Entities;
public partial class Guest
{
    public int GuestId { get; set; }
    public int? Foreigner { get; set; }
    public string? Name { get; set; }
    public string? Fam { get; set; }
    public string? Otch { get; set; }

    public string? Sex { get; set; }

    public string? BirthDay { get; set; } = "  .  .    ";

    public string? BirthCountryId { get; set; } = string.Empty;

    public string? BirthRegion { get; set; } = string.Empty;

    public string? BirthArea { get; set; } = string.Empty;

    public string? BirthCity { get; set; } = string.Empty;

    public string? BirthPoint { get; set; } = string.Empty;

    public string? CitizenshipId { get; set; } = string.Empty;

    public string? TypeDocId { get; set; } = string.Empty;

    public string? DocSeries { get; set; } = string.Empty;

    public string? DocNo { get; set; } = string.Empty;

    public string? DocIssuedDate { get; set; } = "  .  .    ";

    public string? DocValidity { get; set; } = "  .  .    ";

    public string? DocIssuedId { get; set; } = "0";

    public string? RegRegion { get; set; } = string.Empty;

    public string? RegArea { get; set; } = string.Empty;

    public string? RegCity { get; set; } = string.Empty;

    public string? RegPoint { get; set; } = string.Empty;

    public string? RegStreet { get; set; } = string.Empty;

    public string? RegBuilding { get; set; } = string.Empty;

    public string? RegBuilding2 { get; set; } = string.Empty;

    public string? RegApartment { get; set; } = string.Empty;

    public string? ResRegion { get; set; } = string.Empty;

    public string? ResArea { get; set; } = string.Empty;

    public string? ResCity { get; set; } = string.Empty;

    public string? ResPoint { get; set; } = string.Empty;

    public string? ResStreet { get; set; } = string.Empty;

    public string? ResBuilding { get; set; } = string.Empty;

    public string? ResBuilding2 { get; set; } = string.Empty;

    public string? ResApartment { get; set; } = string.Empty;

    public string? RstypeDocId { get; set; }

    public string? RsdocSeries { get; set; }

    public string? RsdocNum { get; set; }

    public string? RsdateIssue { get; set; }

    public string? RsdateIn { get; set; }

    public string? RsdateOut { get; set; }

    public string? RstypeVisaCatId { get; set; }

    public string? RsvisaMulId { get; set; }

    public string? RsvisaId { get; set; }

    public string? Mcseries { get; set; }

    public string? Mcnum { get; set; }

    public string? MckppId { get; set; }

    public string? McdateIn { get; set; }

    public string? McdateOut { get; set; }

    public string? Lrfam { get; set; }

    public string? Lrname { get; set; }

    public string? Lrotch { get; set; }

    public string? LrbirthDay { get; set; }

    public string? LrtypeDocId { get; set; }

    public string? LrdocSeries { get; set; }

    public string? LrdocNum { get; set; }

    public int? RoomId { get; set; }

    public string? EntryGoalId { get; set; }

    public string? VisitPurposeCode { get; set; }

    public string? MigProgCode { get; set; }

    public string? ProfessionId { get; set; }

    public string? LrdocIssueDate { get; set; }

    public string? Phone { get; set; }

    public bool? CardIssued { get; set; } = false;

    public bool? CardHanded { get; set; } = false;

    public int? CardId { get; set; }

    public int? ActionId { get; set; }

    public DateTime? CardIssuedTime { get; set; }

    public DateTime? CardHandedTime { get; set; }

    public int? GuserId { get; set; }

    public DateTime? TimeStamp { get; set; } = DateTime.Now;

    public int? TurGrId { get; set; } = 0;

    public bool? InRoom { get; set; } = true;

    public bool? Exported { get; set; } = true;

    public int DeleteMark { get; set; }

    public int? SigurId { get; set; }
}
