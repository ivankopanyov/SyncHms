namespace SyncHms.Opera.Entities.Tables;

public partial class PendingFtMsgs
{
    public string? Resort { get; set; }
    public decimal? MessageId { get; set; }
    public decimal? TrxNo { get; set; }
    public string? ApprovalStatus { get; set; }
    public string? ApprovalCode { get; set; }
    public string? MessageText { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<PendingFtMsgs>(entity =>
        {
            entity.HasKey(e => e.MessageId)
                .HasName("PENDING_FT_MSG_PK");

            entity.ToTable("PENDING_FT_MSGS");

            entity.HasIndex(e => e.TrxNo)
                .HasName("PENDING_FT_MSGS_TRXNO_IND");

            entity.Property(e => e.MessageId)
                .HasColumnName("MESSAGE_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ApprovalCode)
                .HasColumnName("APPROVAL_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ApprovalStatus)
                .HasColumnName("APPROVAL_STATUS")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MessageText)
                .HasColumnName("MESSAGE_TEXT")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.TrxNo)
                .HasColumnName("TRX_NO")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");
        });
	}
}
