namespace SyncHms.Services.Options;

public class OperaOptions
{
    public const string Section = "Opera";

    [Required(AllowEmptyStrings =  true)]
    public string ConnectionString { get; set; } = string.Empty;

    [Description("Совместимость с версией Oracle Database. Поддерживаются версии 11 и 12.")]
    [Required(AllowEmptyStrings = true), RegularExpression(@"^(11|12)?$")]
    public string? OracleSqlCompatibility { get; set; } = string.Empty;

    public override int GetHashCode() => HashCode.Combine(ConnectionString, OracleSqlCompatibility);

    public override bool Equals(object? obj) => 
        obj is OperaOptions other
        && ConnectionString == other.ConnectionString
        && OracleSqlCompatibility == other.OracleSqlCompatibility;
}
