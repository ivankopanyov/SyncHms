namespace SyncHms.Services.Options;

/// <summary>
/// Класс, описывающий модель опций для установления подключения с удаленному сервису <c>MICROS</c>
/// </summary>
public class MicrosOptions
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "Micros";

    /// <summary>Конечная точка для подключения.</summary>
    [Required(AllowEmptyStrings = true)]
    public string Endpoint { get; set; } = string.Empty;

    public override int GetHashCode() => HashCode.Combine(Endpoint);

    /// <summary>Метод, переопределяющий базовый метод <see cref="Equals"/></summary>
    /// <param name="obj">Объект для сравнения.</param>
    /// <returns>
    /// Если возвращается значение <c>false</c>,
    /// сервис инициирует переподключение к удаленному сервису с новыми опциями.
    /// </returns>
    public override bool Equals(object? obj) => 
        obj is MicrosOptions other && Endpoint == other.Endpoint;
}
