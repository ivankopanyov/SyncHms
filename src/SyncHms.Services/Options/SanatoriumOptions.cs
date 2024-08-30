namespace SyncHms.Services.Options;

/// <summary>
/// Класс, описывающий модель опций для установления подключения с шине данных <c>Sanatorium</c>
/// </summary>
public class SanatoriumOptions
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "Sanatorium";

    /// <summary>
    /// Флаг, определяющий разрешение на попытки установления соединения с шиной данных <c>Sanatorium</c>
    /// </summary>
    public bool Enabled { get; set; }
    
    /// <summary>Строка подлючения к шине данных <c>Sanatorium</c></summary>
    [Required(AllowEmptyStrings = true)]
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>Конечная точка для подлючения к шине данных <c>Sanatorium</c></summary>
    [Required(AllowEmptyStrings = true)]
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>Тело лицензии библиотеки <c>NServiceBus</c></summary>
    [Required(AllowEmptyStrings = true)]
    public string License { get; set; } = string.Empty;

    public override int GetHashCode() =>
        HashCode.Combine(
            Enabled,
            ConnectionString,
            Endpoint,
            License);

    /// <summary>Метод, переопределяющий базовый метод <see cref="Equals"/></summary>
    /// <param name="obj">Объект для сравнения.</param>
    /// <returns>
    /// Если возвращается значение <c>false</c>,
    /// сервис инициирует переподключение к удаленному сервису с новыми опциями.
    /// </returns>
    public override bool Equals(object? obj) => 
        obj is SanatoriumOptions other
        && Enabled == other.Enabled
        && ConnectionString == other.ConnectionString
        && Endpoint == other.Endpoint
        && License == other.License;
}
