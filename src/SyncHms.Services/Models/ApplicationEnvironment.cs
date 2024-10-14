namespace SyncHms.Services;

/// <summary>Класс, описывающий объект, хранящий переменные окружения и их значения.</summary>
public class ApplicationEnvironment
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "Environment";

    /// <summary>Код отеля в базе данных <c>OPERA</c></summary>
    [Required(AllowEmptyStrings = true), Description("Код отеля в базе данных OPERA.")]
    public string ResortCode { get; set; } = string.Empty;

    /// <summary>Идентификатор кассира.</summary>
    [Description("Идентификатор кассира.")]
    public decimal Rvc { get; set; }

    /// <summary>
    /// Флаг, указывающий, нужно ли проводить синхронизацию платежей с системой <c>OPERA</c>.
    /// </summary>
    [Description("Сихронизировать платежные начисления с Opera.")]
    public bool SyncPostingOpera { get; set; }

    /// <summary>
    /// Флаг, указывающий, нужно ли проводить синхронизацию платежей с системой <c>MICROS</c>.
    /// </summary>
    [Description("Сихронизировать платежные начисления с MICROS.")]
    public bool SyncPostingMicros { get; set; }

    /// <summary>Номер следующего чека в системе <c>MICROS</c>.</summary>
    [Description("Номер следующего чека в системе MICROS.\nЗначение в диапазоне от 1 до 9999 включительно.")]
    [Range(1, 9999)]
    public int NextMicrosCheckNumber { get; set; } = 1;

    /// <summary>
    /// Имя кастомного поля сервиса Sanatorium, значение которого отображается в комментарии к истории болезни.
    /// </summary>
    [Description("Имя кастомного поля сервиса Sanatorium, значение которого отображается в комментарии к истории болезни.")]
    [Required(AllowEmptyStrings = true)]
    public string SanatoriumCustomField { get; set; } = string.Empty;
    
    /// <summary>
    /// Платежные коды.
    /// </summary>
    [Description("Платежные коды.")]
    [Required, MaxLength(10)]
    public Dictionary<string, bool> TaxCodes { get; set; } = [];
    
    /// <summary>
    /// Коды синхронизируемых пакетов.
    /// </summary>
    [Description("Коды синхронизируемых пакетов.")]
    [Required]
    public HashSet<string> TrxCodes { get; set; } = [];

    /// <summary>
    /// Альтернативные коды типов документов.
    /// </summary>
    [Required, Description("Альтернативные коды типов документов.")]
    public Dictionary<string, string> DocumentTypeAliases { get; set; } = [];

    /// <summary>
    /// Идентификаторы чатов в <c>Telegram</c>
    /// </summary>
    [Required, EnumerableRegularExpression(@"^-?\d{1,19}(/-?\d{1,10})?$")]
    [Description("ID Telegram-чатов в формате chat_id/message_thread_id для отправки логов.\nОтметьте галочкой чаты, в которые нужно отправлять только сообщения об ошибках.")]
    public Dictionary<string, bool> TelegramChats { get; set; } = [];

    public override int GetHashCode() =>
        HashCode.Combine(
            SyncPostingOpera,
            SyncPostingMicros,
            ResortCode,
            Rvc,
            TaxCodes,
            TrxCodes,
            DocumentTypeAliases);

    /// <summary>
    /// Метод, переопределяющий базовый метод <see cref="Equals"/>.
    /// </summary>
    /// <param name="obj">Объект для сравнения.</param>
    /// <returns>
    /// Если возвращается значение <c>true</c>, сообщение об изменении
    /// значений переменных окружения не будет отправлено сервисам.
    /// </returns>
    public override bool Equals(object? obj) => 
        obj is ApplicationEnvironment other
        && SyncPostingOpera == other.SyncPostingOpera
        && SyncPostingMicros == other.SyncPostingMicros
        && ResortCode == other.ResortCode
        && Rvc == other.Rvc
        && TaxCodes.SequenceEqual(other.TaxCodes)
        && TrxCodes.SequenceEqual(other.TrxCodes)
        && DocumentTypeAliases.SequenceEqual(other.DocumentTypeAliases)
        && TelegramChats.SequenceEqual(other.TelegramChats);
}