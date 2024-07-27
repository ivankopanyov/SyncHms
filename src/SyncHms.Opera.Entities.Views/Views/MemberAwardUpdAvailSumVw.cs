namespace SyncHms.Opera.Entities.Views;
	
public partial class MemberAwardUpdAvailSumVw
{
    public string? Application { get; set; }
    public string? MembershipType { get; set; }
    public string? AwardType { get; set; }
    public string? Resort { get; set; }
    public string? FromRoom { get; set; }
    public string? ToRoom { get; set; }
    public string? FromRoomLabel { get; set; }
    public string? ToRoomLabel { get; set; }
    public decimal? TotalPointsRequired { get; set; }
    public decimal? TotalCancelPenaltyPoints { get; set; }
    public decimal? CancelPenaltyDays { get; set; }
    public decimal? CancelPenaltyCharge { get; set; }
    public string? CancelPenaltyType { get; set; }
    public DateTime? SellBeginDate { get; set; }
    public DateTime? SellEndDate { get; set; }
    public decimal? TotalOpr { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<MemberAwardUpdAvailSumVw>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("MEMBER_AWARD_UPD_AVAIL_SUM_VW");

            entity.Property(e => e.Application)
                .HasColumnName("APPLICATION")
                .HasColumnType("CHAR(3)");

            entity.Property(e => e.AwardType)
                .HasColumnName("AWARD_TYPE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.CancelPenaltyCharge)
                .HasColumnName("CANCEL_PENALTY_CHARGE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CancelPenaltyDays)
                .HasColumnName("CANCEL_PENALTY_DAYS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CancelPenaltyType)
                .HasColumnName("CANCEL_PENALTY_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.FromRoom)
                .HasColumnName("FROM_ROOM")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.FromRoomLabel)
                .HasColumnName("FROM_ROOM_LABEL")
                .IsUnicode(false);

            entity.Property(e => e.MembershipType)
                .HasColumnName("MEMBERSHIP_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.SellBeginDate)
                .HasColumnName("SELL_BEGIN_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.SellEndDate)
                .HasColumnName("SELL_END_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.ToRoom)
                .HasColumnName("TO_ROOM")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ToRoomLabel)
                .HasColumnName("TO_ROOM_LABEL")
                .IsUnicode(false);

            entity.Property(e => e.TotalCancelPenaltyPoints)
                .HasColumnName("TOTAL_CANCEL_PENALTY_POINTS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TotalOpr)
                .HasColumnName("TOTAL_OPR")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TotalPointsRequired)
                .HasColumnName("TOTAL_POINTS_REQUIRED")
                .HasColumnType("NUMBER");
        });
	}
}
