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

    [Required(AllowEmptyStrings = true)]
    [Description("Имя категории, к которой будут относится скидки. Если значение не установлено, то скидка будет вычтена из стоимости.")]
    public string DiscountCategoryName { get; set; } = string.Empty;
    
    [Required(AllowEmptyStrings = true)]
    [Description("Имя категории, к которой будут относится наценки. Если значение не установлено, то наценка будет прибавлена к стоимости.")]
    public string IncreaseCategoryName { get; set; } = string.Empty;

    [Required]
    [Description("Соответствие типов оплат IIKO и OPERA.")]
    public Dictionary<string, string> PaymentTypes { get; set; } = [];

    [Required]
    [Description("Соответствие точки продаж IIKO и FIAS.")]
    public Dictionary<string, int> SalesOutlet { get; set; } = [];

    [Required, MaxLength(10)]
    [Description("Список категорий товаров IIKO в порядке кодов оплат в интерфейсе FIAS. Максимум 10 позиций.")]
    public List<string> Categories { get; set; } = [];

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