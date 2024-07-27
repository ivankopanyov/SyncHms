namespace SyncHms.Opera.Entities.Views;
	
public partial class SidBofDefRecordColumns
{
    public decimal? BofRecId { get; set; }
    public decimal? BofColId { get; set; }
    public decimal? MainSeqNo { get; set; }
    public decimal? SeqNo { get; set; }
    public decimal? BofIntfCode { get; set; }
    public string? Resort { get; set; }
    public string? ColumnName { get; set; }
    public decimal? ColumnLength { get; set; }
    public string? DataType { get; set; }
    public string? Format { get; set; }
    public string? AmtColumn { get; set; }
    public DateTime? InsertDate { get; set; }
    public string? InsertUser { get; set; }
    public string? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? AlignLeftRight { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<SidBofDefRecordColumns>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("SID_BOF$DEF_RECORD_COLUMNS");

            entity.Property(e => e.AlignLeftRight)
                .HasColumnName("ALIGN_LEFT_RIGHT")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AmtColumn)
                .HasColumnName("AMT_COLUMN")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.BofColId)
                .HasColumnName("BOF_COL_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BofIntfCode)
                .HasColumnName("BOF_INTF_CODE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BofRecId)
                .HasColumnName("BOF_REC_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ColumnLength)
                .HasColumnName("COLUMN_LENGTH")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ColumnName)
                .HasColumnName("COLUMN_NAME")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.DataType)
                .HasColumnName("DATA_TYPE")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Format)
                .HasColumnName("FORMAT")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.MainSeqNo)
                .HasColumnName("MAIN_SEQ_NO")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.SeqNo)
                .HasColumnName("SEQ_NO")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasMaxLength(200)
                .IsUnicode(false);
        });
	}
}
