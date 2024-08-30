namespace SyncHms.Opera.Entities.Views;
	
public partial class OcrmProfileSegBatchesVw
{
    public decimal? SegId { get; set; }
    public string? SegCode { get; set; }
    public string? SegDesc { get; set; }
    public string? SegSourceType { get; set; }
    public string? Resort { get; set; }
    public decimal? SegBatchId { get; set; }
    public string? JobNameId { get; set; }
    public DateTime? BatchStartDate { get; set; }
    public DateTime? BatchEndDate { get; set; }
    public string? Status { get; set; }
    public decimal? RecordCount { get; set; }
    public DateTime? InactiveDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? EspBatchCode { get; set; }
    public decimal? ParamListId { get; set; }
    public decimal? TaskInstId { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<OcrmProfileSegBatchesVw>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("OCRM_PROFILE_SEG_BATCHES_VW");

            entity.Property(e => e.BatchEndDate)
                .HasColumnName("BATCH_END_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.BatchStartDate)
                .HasColumnName("BATCH_START_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.EspBatchCode)
                .HasColumnName("ESP_BATCH_CODE")
                .HasMaxLength(240)
                .IsUnicode(false);

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.JobNameId)
                .HasColumnName("JOB_NAME_ID")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.ParamListId)
                .HasColumnName("PARAM_LIST_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.RecordCount)
                .HasColumnName("RECORD_COUNT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.SegBatchId)
                .HasColumnName("SEG_BATCH_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.SegCode)
                .IsRequired()
                .HasColumnName("SEG_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.SegDesc)
                .HasColumnName("SEG_DESC")
                .IsUnicode(false);

            entity.Property(e => e.SegId)
                .HasColumnName("SEG_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.SegSourceType)
                .IsRequired()
                .HasColumnName("SEG_SOURCE_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Status)
                .HasColumnName("STATUS")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.TaskInstId)
                .HasColumnName("TASK_INST_ID")
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
