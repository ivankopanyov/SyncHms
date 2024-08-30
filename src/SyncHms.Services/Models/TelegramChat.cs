namespace SyncHms.Services;

/// <summary>Класс, описывающий модель чата <c>Telegram</c></summary>
public class TelegramChat
{
    /// <summary>Идентификатор чата.</summary>
    public long Id { get; set; }

    /// <summary>Идентификатор потока сообщений в чате.</summary>
    public int? MessageThreadId { get; set; }

    /// <summary>
    /// Флаг, указывающий, что в чат нужно отправлять только логи событий,
    /// обработка которых была завершена с ошибкой.
    /// </summary>
    public bool OnlyError { get; set; }

    public override string ToString()
    {
        var result = Id.ToString();

        if (MessageThreadId != null)
            result += $"/{MessageThreadId}";

        return result;
    }
}
