namespace SyncHms.OzLocks.Entities;

public partial class RoomAction
{
    public int ActionId { get; set; }

    public int? RoomId { get; set; }

    public DateTime? TimeIn { get; set; }

    public DateTime? TimeOut { get; set; }

    public int? Duration { get; set; }

    public bool? DayHour { get; set; } = false;

    public decimal? FactDeposit { get; set; } = 0;

    public decimal? Summ { get; set; } = 0;

    public DateTime? TimeOutS { get; set; }

    public decimal? SummS { get; set; } = 0;

    public int? HourS { get; set; } = 0;

    public int? DayS { get; set; }

    public int? AuserId { get; set; }

    public DateTime? TimeStamp { get; set; } = DateTime.Now;

    public bool? Used { get; set; } = false;

    public int? LastProlongType { get; set; } = 0;

    public int? Day1 { get; set; } = 0;

    public int? Hour1 { get; set; } = 0;

    public decimal? Summa1 { get; set; } = 0;

    public DateTime? TimeIn1 { get; set; }

    public DateTime? TimeOut1 { get; set; }

    public int? Day2 { get; set; } = 0;

    public int? Hour2 { get; set; } = 0;

    public decimal? Summa2 { get; set; } = 0;

    public DateTime? TimeIn2 { get; set; }

    public DateTime? TimeOut2 { get; set; }

    public DateTime? FactTimeOut { get; set; }

    public decimal? FactMoney { get; set; }

    public int? FactDay { get; set; }

    public int? FactHour { get; set; }

    public int? DopMesta { get; set; } = 0;

    public int? DopMesta1 { get; set; }

    public int? DopMesta2 { get; set; }
}
