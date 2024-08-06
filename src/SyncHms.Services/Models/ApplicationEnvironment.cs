namespace SyncHms.Services;

public class ApplicationEnvironment
{
    public const string Section = "Environment";

    [Required(AllowEmptyStrings = true), Description("Код отеля в базе данных OPERA.")]
    public string ResortCode { get; set; } = string.Empty;

    public decimal Rvc { get; set; }

    [Description("Сихронизировать платежные начисления с Opera.")]
    public bool SyncPostingOpera { get; set; }

    [Description("Сихронизировать платежные начисления с MICROS.")]
    public bool SyncPostingMicros { get; set; }

    [Required, MaxLength(10)]
    public Dictionary<string, bool> TaxCodes { get; set; } = [];

    [Required]
    public HashSet<string> TrxCodes { get; set; } = [];

    [Required, Description("Альтернативные коды типов документов.")]
    public Dictionary<string, string> DocumentTypeAliases { get; set; } = [];

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