namespace SyncHms.Opera.Entities.Tables;

public partial class ScAttachment
{
    public ScAttachment()
    {
        RequestTypeTemplates = new HashSet<RequestTypeTemplates>();
    }

    public decimal? AttachId { get; set; }
    public string? LinkType { get; set; }
    public decimal? LinkId { get; set; }
    public string? Resort { get; set; }
    public string? BlobType { get; set; }
    public byte[] BlobItem { get; set; }
    public string? Description { get; set; }
    public string? Filename { get; set; }
    public string? ImgType { get; set; }
    public byte? LaptopChange { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? Language { get; set; }
    public decimal? Owner { get; set; }
    public string? GlobalYn { get; set; }
    public string? TemplateType { get; set; }
    public decimal? OrderBy { get; set; }

    public virtual Resort ResortNavigation { get; set; }
    public virtual ICollection<RequestTypeTemplates> RequestTypeTemplates { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ScAttachment>(entity =>
        {
            entity.HasKey(e => e.AttachId)
                .HasName("SA_PK");

            entity.ToTable("SC$ATTACHMENT");

            entity.HasIndex(e => e.LinkId)
                .HasName("SA_LINK_ID_IDX");

            entity.HasIndex(e => e.Resort)
                .HasName("SA_RESORT_IDX");

            entity.Property(e => e.AttachId)
                .HasColumnName("ATTACH_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.BlobItem)
                .HasColumnName("BLOB_ITEM")
                .HasColumnType("BLOB");

            entity.Property(e => e.BlobType)
                .IsRequired()
                .HasColumnName("BLOB_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.Filename)
                .HasColumnName("FILENAME")
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.GlobalYn)
                .HasColumnName("GLOBAL_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ImgType)
                .HasColumnName("IMG_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Language)
                .HasColumnName("LANGUAGE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.LaptopChange)
                .HasColumnName("LAPTOP_CHANGE")
                .HasColumnType("NUMBER(2)");

            entity.Property(e => e.LinkId)
                .HasColumnName("LINK_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.LinkType)
                .IsRequired()
                .HasColumnName("LINK_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Owner)
                .HasColumnName("OWNER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.TemplateType)
                .HasColumnName("TEMPLATE_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

			if (!types.Contains(typeof(Resort)))
				entity.Ignore(e => e.ResortNavigation);
			else
	            entity.HasOne(d => d.ResortNavigation)
	                .WithMany(p => p.ScAttachment)
	                .HasForeignKey(d => d.Resort)
	                .OnDelete(DeleteBehavior.ClientSetNull)
	                .HasConstraintName("SA_RESORT_FK");
        
			if (!types.Contains(typeof(RequestTypeTemplates)))
				entity.Ignore(e => e.RequestTypeTemplates);
		});
	}
}
