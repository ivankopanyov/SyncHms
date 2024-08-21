namespace SyncHms.Services.Options;

/// <summary>
/// Класс, описывающий модель опций для установления подключения с сервисом API бота <c>Telegram</c>
/// </summary>
public class TelegramBotOptions
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "Telegram";

    /// <summary>
    /// Флаг, определяющий разрешение на попытки установления соединения с сервисом API бота <c>Telegram</c>
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>Токен, выданный боту сервисом <c>Telegram</c></summary>
    [Required(AllowEmptyStrings = true)]
    public string Token { get; set; } = string.Empty;

    public override int GetHashCode() => HashCode.Combine(Enabled, Token);

    /// <summary>Метод, переопределяющий базовый метод <see cref="Equals"/></summary>
    /// <param name="obj">Объект для сравнения.</param>
    /// <returns>
    /// Если возвращается значение <c>false</c>,
    /// сервис инициирует переподключение к удаленному сервису с новыми опциями.
    /// </returns>
    public override bool Equals(object? obj) => 
        obj is TelegramBotOptions other
        && Enabled == other.Enabled
        && Token == other.Token;
}
