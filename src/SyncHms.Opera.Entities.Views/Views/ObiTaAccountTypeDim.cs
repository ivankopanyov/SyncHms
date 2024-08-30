namespace SyncHms.Opera.Entities.Views;
	
public partial class ObiTaAccountTypeDim
{
    public string? AllCode { get; set; }
    public string? AllDesc { get; set; }
    public string? AccountType { get; set; }
    public string? AccountTypeDesc { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ObiTaAccountTypeDim>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("OBI_TA_ACCOUNT_TYPE_DIM");

            entity.Property(e => e.AccountType)
                .HasColumnName("ACCOUNT_TYPE")
                .IsUnicode(false);

            entity.Property(e => e.AccountTypeDesc)
                .HasColumnName("ACCOUNT_TYPE_DESC")
                .IsUnicode(false);

            entity.Property(e => e.AllCode)
                .HasColumnName("ALL_CODE")
                .IsUnicode(false);

            entity.Property(e => e.AllDesc)
                .HasColumnName("ALL_DESC")
                .IsUnicode(false);
        });
	}
}
