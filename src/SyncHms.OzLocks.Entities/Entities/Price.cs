namespace SyncHms.OzLocks.Entities;

public partial class Price
{
    public int Id { get; set; }

    public int? TypeRoomId { get; set; }

    public decimal? PriceDay { get; set; }

    public decimal? PriceHour { get; set; }

    public decimal? PriceHourOv { get; set; }

    public int? UserId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public DateTime? StartPeriod { get; set; }

    public DateTime? EndPeriod { get; set; }

    public int? TypePeriod { get; set; }
}
