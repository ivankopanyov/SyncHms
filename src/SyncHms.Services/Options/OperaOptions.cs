namespace SyncHms.Services.Options;

/// <summary>
/// Класс, описывающий модель опций для установления подключения к базе данных <c>OPERA</c>
/// </summary>
public class OperaOptions
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "Opera";

    /// <summary>Строка подключения к базе данных <c>ORACLE</c></summary>
    [Required(AllowEmptyStrings =  true)]
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>Версия базы данных <c>ORACLE</c> для обеспечивания совместимости.</summary>
    [Description("Совместимость с версией Oracle Database. Поддерживаются версии 11 и 12.")]
    [Required(AllowEmptyStrings = true), RegularExpression(@"^(11|12)?$")]
    public string? OracleSqlCompatibility { get; set; } = string.Empty;

    public override int GetHashCode() => HashCode.Combine(ConnectionString, OracleSqlCompatibility);

    /// <summary>Метод, переопределяющий базовый метод <see cref="Equals"/></summary>
    /// <param name="obj">Объект для сравнения.</param>
    /// <returns>
    /// Если возвращается значение <c>false</c>,
    /// сервис инициирует переподключение к удаленному сервису с новыми опциями.
    /// </returns>
    public override bool Equals(object? obj) => 
        obj is OperaOptions other
        && ConnectionString == other.ConnectionString
        && OracleSqlCompatibility == other.OracleSqlCompatibility;
}
