namespace SyncHms.Opera.Entities.Tables;

public partial class AqBeQueueTableI
{
    public decimal? Subscriber { get; set; }
    public string? Name { get; set; }
    public decimal? Queue { get; set; }
    public decimal? MsgPriority { get; set; }
    public DateTime? MsgEnqTime { get; set; }
    public decimal? MsgStepNo { get; set; }
    public decimal? MsgChainNo { get; set; }
    public decimal? MsgLocalOrderNo { get; set; }
    public Guid Msgid { get; set; }
    public string? Hint { get; set; }
    public Guid? Spare { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<AqBeQueueTableI>(entity =>
        {
            entity.HasKey(e => new { e.Subscriber, e.Name, e.Queue, e.MsgPriority, e.MsgEnqTime, e.MsgStepNo, e.MsgChainNo, e.MsgLocalOrderNo, e.Msgid })
                .HasName("SYS_IOT_TOP_142124");

            entity.ToTable("AQ$_BE_QUEUE_TABLE_I");

            entity.Property(e => e.Subscriber)
                .HasColumnName("SUBSCRIBER#")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Name)
                .HasColumnName("NAME")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.Queue)
                .HasColumnName("QUEUE#")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MsgPriority)
                .HasColumnName("MSG_PRIORITY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MsgEnqTime)
                .HasColumnName("MSG_ENQ_TIME")
                .HasColumnType("TIMESTAMP(6)");

            entity.Property(e => e.MsgStepNo)
                .HasColumnName("MSG_STEP_NO")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MsgChainNo)
                .HasColumnName("MSG_CHAIN_NO")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MsgLocalOrderNo)
                .HasColumnName("MSG_LOCAL_ORDER_NO")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Msgid).HasColumnName("MSGID");

            entity.Property(e => e.Hint)
                .HasColumnName("HINT")
                .HasColumnType("ROWID");

            entity.Property(e => e.Spare).HasColumnName("SPARE");
        });
	}
}
