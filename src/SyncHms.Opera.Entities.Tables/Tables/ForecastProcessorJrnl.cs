namespace SyncHms.Opera.Entities.Tables;

public partial class ForecastProcessorJrnl
{
    public decimal? JrnlId { get; set; }
    public decimal? FpSeq { get; set; }
    public decimal? FpSid { get; set; }
    public decimal? FpInstId { get; set; }
    public string? FpSchema { get; set; }
    public string? FpSourceSchema { get; set; }
    public DateTime? StartupTime { get; set; }
    public DateTime? ShutdownTime { get; set; }
    public string? FpCommand { get; set; }
    public decimal? NrProcessedMsg { get; set; }
    public string? ActionType { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ForecastProcessorJrnl>(entity =>
        {
            entity.HasKey(e => e.JrnlId)
                .HasName("FP_JRNL_PK");

            entity.ToTable("FORECAST_PROCESSOR_JRNL");

            entity.HasIndex(e => e.FpSeq)
                .HasName("FP_SEQ_IDX");

            entity.Property(e => e.JrnlId)
                .HasColumnName("JRNL_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ActionType)
                .HasColumnName("ACTION_TYPE")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.FpCommand)
                .HasColumnName("FP_COMMAND")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.FpInstId)
                .HasColumnName("FP_INST_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.FpSchema)
                .HasColumnName("FP_SCHEMA")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.FpSeq)
                .HasColumnName("FP_SEQ")
                .HasColumnType("NUMBER");

            entity.Property(e => e.FpSid)
                .HasColumnName("FP_SID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.FpSourceSchema)
                .HasColumnName("FP_SOURCE_SCHEMA")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.NrProcessedMsg)
                .HasColumnName("NR_PROCESSED_MSG")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ShutdownTime)
                .HasColumnName("SHUTDOWN_TIME")
                .HasColumnType("DATE");

            entity.Property(e => e.StartupTime)
                .HasColumnName("STARTUP_TIME")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");
        });
	}
}
