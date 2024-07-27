namespace SyncHms.Opera.Entities.Tables;

public partial class OcrmProfileSegDtl
{
    public decimal? RecordId { get; set; }
    public decimal? SegBatchId { get; set; }
    public string? RecordLocator { get; set; }
    public decimal? OcrmProfileId { get; set; }
    public string? EspRecordLocator { get; set; }
    public string? TargetEmail { get; set; }
    public string? TargetEmailFormatType { get; set; }
    public string? IndexEmail { get; set; }
    public string? TemplateCode { get; set; }
    public string? LanguageCode { get; set; }
    public string? SendToEspStatusFlg { get; set; }
    public string? SendToEspProcessMsg { get; set; }
    public string? EspReplyStatus { get; set; }
    public DateTime? EspReplyDate { get; set; }
    public DateTime? InactiveDate { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public decimal? ParamListId { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<OcrmProfileSegDtl>(entity =>
        {
            entity.HasKey(e => e.RecordId)
                .HasName("OPL_PK");

            entity.ToTable("OCRM_PROFILE_SEG_DTL");

            entity.HasIndex(e => e.OcrmProfileId)
                .HasName("OCRM_SEG_DTL_IDX02");

            entity.HasIndex(e => e.RecordLocator)
                .HasName("OCRM_SEG_DTL_UK01")
                .IsUnique();

            entity.HasIndex(e => new { e.IndexEmail, e.SegBatchId })
                .HasName("OCRM_SEG_DTL_IDX03");

            entity.HasIndex(e => new { e.SegBatchId, e.RecordId })
                .HasName("OCRM_SEG_DTL_IDX01");

            entity.Property(e => e.RecordId)
                .HasColumnName("RECORD_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.EspRecordLocator)
                .HasColumnName("ESP_RECORD_LOCATOR")
                .HasMaxLength(240)
                .IsUnicode(false);

            entity.Property(e => e.EspReplyDate)
                .HasColumnName("ESP_REPLY_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.EspReplyStatus)
                .HasColumnName("ESP_REPLY_STATUS")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.IndexEmail)
                .HasColumnName("INDEX_EMAIL")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.LanguageCode)
                .HasColumnName("LANGUAGE_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.OcrmProfileId)
                .HasColumnName("OCRM_PROFILE_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ParamListId)
                .HasColumnName("PARAM_LIST_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.RecordLocator)
                .HasColumnName("RECORD_LOCATOR")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.SegBatchId)
                .HasColumnName("SEG_BATCH_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.SendToEspProcessMsg)
                .HasColumnName("SEND_TO_ESP_PROCESS_MSG")
                .IsUnicode(false);

            entity.Property(e => e.SendToEspStatusFlg)
                .HasColumnName("SEND_TO_ESP_STATUS_FLG")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.TargetEmail)
                .HasColumnName("TARGET_EMAIL")
                .IsUnicode(false);

            entity.Property(e => e.TargetEmailFormatType)
                .HasColumnName("TARGET_EMAIL_FORMAT_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.TemplateCode)
                .HasColumnName("TEMPLATE_CODE")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");
        });
	}
}
