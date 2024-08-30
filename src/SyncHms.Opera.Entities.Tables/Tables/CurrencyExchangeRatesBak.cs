namespace SyncHms.Opera.Entities.Tables;

public partial class CurrencyExchangeRatesBak
{
    public DateTime? BeginDate { get; set; }
    public decimal? ExchangeRate { get; set; }
    public string? LockRateYn { get; set; }
    public string? Comments { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? Resort { get; set; }
    public string? ExchangeRateType { get; set; }
    public string? BaseCurrCode { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal? CurrActionId { get; set; }

    public virtual CurrencyExchangeRateType CurrencyExchangeRateType { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<CurrencyExchangeRatesBak>(entity =>
        {
            entity.HasKey(e => new { e.Resort, e.BaseCurrCode, e.CurrencyCode, e.ExchangeRateType, e.BeginDate })
                .HasName("OCO_970088");

            entity.ToTable("CURRENCY_EXCHANGE_RATES_BAK");

            entity.HasIndex(e => new { e.Resort, e.ExchangeRateType })
                .HasName("EXCHANGE_RT_FK_I");

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.BaseCurrCode)
                .HasColumnName("BASE_CURR_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CurrencyCode)
                .HasColumnName("CURRENCY_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ExchangeRateType)
                .HasColumnName("EXCHANGE_RATE_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.BeginDate)
                .HasColumnName("BEGIN_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.Comments)
                .HasColumnName("COMMENTS")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.CurrActionId)
                .HasColumnName("CURR_ACTION_ID")
                .HasColumnType("NUMBER(20)");

            entity.Property(e => e.ExchangeRate)
                .HasColumnName("EXCHANGE_RATE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.LockRateYn)
                .IsRequired()
                .HasColumnName("LOCK_RATE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

			if (!types.Contains(typeof(CurrencyExchangeRateType)))
				entity.Ignore(e => e.CurrencyExchangeRateType);
			else
	            entity.HasOne(d => d.CurrencyExchangeRateType)
	                .WithMany(p => p.CurrencyExchangeRatesBak)
	                .HasForeignKey(d => new { d.Resort, d.ExchangeRateType })
	                .OnDelete(DeleteBehavior.ClientSetNull)
	                .HasConstraintName("OCO_970098");
        });
	}
}
