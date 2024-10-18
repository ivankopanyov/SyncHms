namespace SyncHms.OzLocks.Entities;

public partial class Guest
{
    public int GuestId { get; set; }

    public int? Foreigner { get; set; }

    public string? Name { get; set; }

    public string? Fam { get; set; }

    public string? Otch { get; set; }

    public string? Sex { get; set; }

    public string? BirthDay { get; set; }

    public string? BirthCountryId { get; set; }

    public string? BirthRegion { get; set; }

    public string? BirthArea { get; set; }

    public string? BirthCity { get; set; }

    public string? BirthPoint { get; set; }

    public string? CitizenshipId { get; set; }

    public string? TypeDocId { get; set; }

    public string? DocSeries { get; set; }

    public string? DocNo { get; set; }

    public string? DocIssuedDate { get; set; }

    public string? DocValidity { get; set; }

    public string? DocIssuedId { get; set; }

    public string? RegRegion { get; set; }

    public string? RegArea { get; set; }

    public string? RegCity { get; set; }

    public string? RegPoint { get; set; }

    public string? RegStreet { get; set; }

    public string? RegBuilding { get; set; }

    public string? RegBuilding2 { get; set; }

    public string? RegApartment { get; set; }

    public string? ResRegion { get; set; }

    public string? ResArea { get; set; }

    public string? ResCity { get; set; }

    public string? ResPoint { get; set; }

    public string? ResStreet { get; set; }

    public string? ResBuilding { get; set; }

    public string? ResBuilding2 { get; set; }

    public string? ResApartment { get; set; }

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

    public bool? CardIssued { get; set; }

    public bool? CardHanded { get; set; }

    public int? CardId { get; set; }

    public int? ActionId { get; set; }

    public DateTime? CardIssuedTime { get; set; }

    public DateTime? CardHandedTime { get; set; }

    public int? GuserId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public int? TurGrId { get; set; }

    public bool? InRoom { get; set; }

    public bool? Exported { get; set; }

    public int DeleteMark { get; set; }

    public int? SigurId { get; set; }
}
