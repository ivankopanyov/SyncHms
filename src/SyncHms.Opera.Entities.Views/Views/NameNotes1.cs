namespace SyncHms.Opera.Entities.Views;
	
public partial class NameNotes1
{
    public string? Resort { get; set; }
    public decimal? NoteId { get; set; }
    public decimal? NameId { get; set; }
    public string? NoteCode { get; set; }
    public string? NoteTitle { get; set; }
    public string? InternalYn { get; set; }
    public string? ConfidentialYn { get; set; }
    public string? Notes { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? ActionType { get; set; }
    public DateTime? ActionDueDate { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<NameNotes1>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("NAME_NOTES");

            entity.Property(e => e.ActionDueDate)
                .HasColumnName("ACTION_DUE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.ActionType)
                .HasColumnName("ACTION_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ConfidentialYn)
                .HasColumnName("CONFIDENTIAL_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.InternalYn)
                .HasColumnName("INTERNAL_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.NameId)
                .HasColumnName("NAME_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.NoteCode)
                .HasColumnName("NOTE_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.NoteId)
                .HasColumnName("NOTE_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.NoteTitle)
                .HasColumnName("NOTE_TITLE")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.Notes)
                .HasColumnName("NOTES")
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
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
