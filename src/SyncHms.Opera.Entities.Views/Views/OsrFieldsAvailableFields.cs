namespace SyncHms.Opera.Entities.Views;
	
public partial class OsrFieldsAvailableFields
{
    public decimal? Reportid { get; set; }
    public decimal? Fieldno { get; set; }
    public decimal? Fieldid { get; set; }
    public string? Fieldtype { get; set; }
    public string? Summary { get; set; }
    public string? Label { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? Fontsize { get; set; }
    public string? Fontname { get; set; }
    public string? BoldYn { get; set; }
    public string? ItalicYn { get; set; }
    public string? UnderlineYn { get; set; }
    public string? Alignment { get; set; }
    public string? Type { get; set; }
    public decimal? Groupno { get; set; }
    public string? Content { get; set; }
    public string? Alias { get; set; }
    public string? Reserve { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? Format { get; set; }
    public string? Description { get; set; }
    public string? ALabel { get; set; }
    public string? Source { get; set; }
    public string? Link { get; set; }
    public decimal? AWidth { get; set; }
    public string? AAlignment { get; set; }
    public string? Parametertype { get; set; }
    public string? Selectlov { get; set; }
    public string? AFormat { get; set; }
    public string? Filter { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<OsrFieldsAvailableFields>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("OSR_FIELDS_AVAILABLE_FIELDS");

            entity.Property(e => e.AAlignment)
                .HasColumnName("A_ALIGNMENT")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.AFormat)
                .HasColumnName("A_FORMAT")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.ALabel)
                .IsRequired()
                .HasColumnName("A_LABEL")
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.Property(e => e.AWidth)
                .HasColumnName("A_WIDTH")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Alias)
                .HasColumnName("ALIAS")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Alignment)
                .HasColumnName("ALIGNMENT")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.BoldYn)
                .HasColumnName("BOLD_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Content)
                .HasColumnName("CONTENT")
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Fieldid)
                .HasColumnName("FIELDID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Fieldno)
                .HasColumnName("FIELDNO")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Fieldtype)
                .IsRequired()
                .HasColumnName("FIELDTYPE")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Filter)
                .HasColumnName("FILTER")
                .IsUnicode(false);

            entity.Property(e => e.Fontname)
                .HasColumnName("FONTNAME")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Fontsize)
                .HasColumnName("FONTSIZE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Format)
                .HasColumnName("FORMAT")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Groupno)
                .HasColumnName("GROUPNO")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Height)
                .HasColumnName("HEIGHT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ItalicYn)
                .HasColumnName("ITALIC_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Label)
                .HasColumnName("LABEL")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.Link)
                .HasColumnName("LINK")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Parametertype)
                .HasColumnName("PARAMETERTYPE")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Reportid)
                .HasColumnName("REPORTID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Reserve)
                .HasColumnName("RESERVE")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Selectlov)
                .HasColumnName("SELECTLOV")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.Source)
                .IsRequired()
                .HasColumnName("SOURCE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.Summary)
                .HasColumnName("SUMMARY")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.Type)
                .HasColumnName("TYPE")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.UnderlineYn)
                .HasColumnName("UNDERLINE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Width)
                .HasColumnName("WIDTH")
                .HasColumnType("NUMBER");
        });
	}
}
