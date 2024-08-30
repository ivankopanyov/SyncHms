namespace SyncHms.Services;

/// <summary>
/// Класс, описывающий контекст подключения к базе данных <c>OPERA</c><br/>
/// Унаследован от класса <see cref="OperaDbContextBase"/>
/// </summary>
public class OperaDbContext : OperaDbContextBase
{
    /// <summary>Строка подключения к базе данных.</summary>
    private readonly string _connectionString;

    /// <summary>Версия базы данных <c>ORACLE</c>, поддерживаемая контекстом.</summary>
    private readonly string? _oracleSqlCompatibility;
    
    public virtual DbSet<ReservationName> ReservationNames { get; set; }

    public virtual DbSet<Name> Names { get; set; }

    public virtual DbSet<Businessdate> Businessdates { get; set; }

    public virtual DbSet<ReservationDailyElementName> ReservationDailyElementNames { get; set; }

    public virtual DbSet<ReservationDailyElements> ReservationDailyElements { get; set; }

    public virtual DbSet<ResortRoomCategory> ResortRoomCategories { get; set; }

    public virtual DbSet<ResortRoomClasses> ResortRoomClasses { get; set; }

    public virtual DbSet<NamePhone> NamePhones { get; set; }

    public virtual DbSet<ReservationProducts> ReservationProducts { get; set; }

    public virtual DbSet<ReservationProductPrices> ReservationProductPrices { get; set; }

    public virtual DbSet<ProductPostingRules> ProductPostingRules { get; set; }

    public virtual DbSet<NameDocuments> NameDocuments { get; set; }

    public virtual DbSet<NameAddress> NameAddresses { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<NameData> NameData { get; set; }

    internal OperaDbContext(string connectionString, string? oracleSqlCompatibility = null)
    {
        _connectionString = connectionString;
        _oracleSqlCompatibility = oracleSqlCompatibility;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        .UseOracle(_connectionString, options =>
        {
            if (!string.IsNullOrWhiteSpace(_oracleSqlCompatibility))
                options.UseOracleSQLCompatibility(_oracleSqlCompatibility); 
        });
}