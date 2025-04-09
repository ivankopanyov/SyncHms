namespace SyncHms.Services.Options;

/// <summary>
/// Класс, описывающий модель опций для установления подключения с удаленному сервису <c>IIKO</c>
/// </summary>
public class IikoOptions
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "Iiko";

    /// <summary>Url для подключения.</summary>
    [Required(AllowEmptyStrings = true)]
    public string Url { get; set; } = string.Empty;

    /// <summary>Логин для подключения.</summary>
    [Required(AllowEmptyStrings = true)]
    [Description("Логин пользователя IIKO.")]
    public string Login { get; set; } = string.Empty;

    /// <summary>Хэш пароля для подключения.</summary>
    [Required(AllowEmptyStrings = true)]
    [Description("Sha1 hash от пароля пользователя IIKO.")]
    public string PasswordHash { get; set; } = string.Empty;
    
    [Required]
    [Description("Соответствие типов оплат IIKO и OPERA.")]
    public Dictionary<string, string> PaymentTypes { get; set; } = [];

    [Required]
    [JsonType<IikoConfiguration>(ErrorMessage = "Некорректный формат конфигураций.")]
    public string ConfigurationJson { get; set; } = JsonConvert.SerializeObject(new RouteConfiguration());

    [Newtonsoft.Json.JsonIgnore]
    public IikoConfiguration RouteConfiguration => JsonConvert.DeserializeObject<IikoConfiguration>(ConfigurationJson)!;

    public override int GetHashCode() => HashCode.Combine(Url, Login, PasswordHash);

    /// <summary>Метод, переопределяющий базовый метод <see cref="Equals"/></summary>
    /// <param name="obj">Объект для сравнения.</param>
    /// <returns>
    /// Если возвращается значение <c>false</c>,
    /// сервис инициирует переподключение к удаленному сервису с новыми опциями.
    /// </returns>
    public override bool Equals(object? obj) => 
        obj is IikoOptions other
        && Url == other.Url
        && Login == other.Login
        && PasswordHash == other.PasswordHash;
}