namespace SyncHms.Services.Options;

/// <summary>
/// Класс, описывающий модель опций для установления подключения с удаленному сервису <c>FIAS</c>
/// </summary>
public class FiasServiceOptions
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "Fias";

    /// <summary>
    /// Флаг, определяющий разрешение на попытки установления соединения с удаленным сервисом <c>FIAS</c>
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>Имя или адрес удаленного хоста для подключения.</summary>
    [Required(AllowEmptyStrings = true)]
    public string Host { get; set; } = string.Empty;

    /// <summary>Номер порта удаленного хоста для подключения.</summary>
    [Range(IPEndPoint.MinPort, IPEndPoint.MaxPort)]
    public int Port { get; set; }

    /// <summary>Код локализации.</summary>
    [Description("Код локализации FIAS.\nЕсли поле пустое или код не будет найден - будет установлена локализация хоста.")]
    [Required(AllowEmptyStrings = true)]
    public string LocalizationCode { get; set; } = string.Empty;

    public override int GetHashCode() => HashCode.Combine(Enabled, Host, Port);

    /// <summary>Метод, переопределяющий базовый метод <see cref="Equals"/></summary>
    /// <param name="obj">Объект для сравнения.</param>
    /// <returns>
    /// Если возвращается значение <c>false</c>,
    /// сервис инициирует переподключение к удаленному сервису с новыми опциями.
    /// </returns>
    public override bool Equals(object? obj) =>
        obj is FiasServiceOptions other
        && Enabled == other.Enabled
        && Host == other.Host
        && Port == other.Port;
}