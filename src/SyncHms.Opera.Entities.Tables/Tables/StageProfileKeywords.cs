namespace SyncHms.Opera.Entities.Tables;

public partial class StageProfileKeywords
{
    public string? Resort { get; set; }
    public string? ResortNameId { get; set; }
    public string? KeywordType { get; set; }
    public string? Keyword { get; set; }
    public string? ValidYn { get; set; }
    public string? ErrorDesc { get; set; }

    public virtual StageProfiles ResortNavigation { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<StageProfileKeywords>(entity =>
        {
            entity.HasKey(e => new { e.Resort, e.ResortNameId, e.KeywordType, e.Keyword })
                .HasName("STAGE_PROFILE_KEYWORDS_PK");

            entity.ToTable("STAGE_PROFILE_KEYWORDS");

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ResortNameId)
                .HasColumnName("RESORT_NAME_ID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.KeywordType)
                .HasColumnName("KEYWORD_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Keyword)
                .HasColumnName("KEYWORD")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.ErrorDesc)
                .HasColumnName("ERROR_DESC")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.ValidYn)
                .HasColumnName("VALID_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

			if (!types.Contains(typeof(StageProfiles)))
				entity.Ignore(e => e.ResortNavigation);
			else
	            entity.HasOne(d => d.ResortNavigation)
	                .WithMany(p => p.StageProfileKeywords)
	                .HasForeignKey(d => new { d.Resort, d.ResortNameId })
	                .OnDelete(DeleteBehavior.ClientSetNull)
	                .HasConstraintName("STAGE_PROFILE_KEYWORDS_FK");
        });
	}
}
