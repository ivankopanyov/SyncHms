namespace SyncHms.Opera.Entities.Views;
	
public partial class MembershipHistoryVw
{
    public decimal? MembershipId { get; set; }
    public string? ChangeType { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public string? Description { get; set; }
    public decimal? ExtractBatchId { get; set; }
    public decimal? ActionId { get; set; }
    public string? ActionType { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<MembershipHistoryVw>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("MEMBERSHIP_HISTORY_VW");

            entity.Property(e => e.ActionId)
                .HasColumnName("ACTION_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ActionType)
                .HasColumnName("ACTION_TYPE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.ChangeType)
                .HasColumnName("CHANGE_TYPE")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.ExtractBatchId)
                .HasColumnName("EXTRACT_BATCH_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MembershipId)
                .HasColumnName("MEMBERSHIP_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.NewValue)
                .HasColumnName("NEW_VALUE")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.OldValue)
                .HasColumnName("OLD_VALUE")
                .HasMaxLength(80)
                .IsUnicode(false);
        });
	}
}
