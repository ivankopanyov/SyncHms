namespace SyncHms.Events.Messages;

/// <summary>Класс, описывающий сообщение, отправляемое ботом <c>Telegram</c></summary>
internal class TelegramMessage
{   
    /// <summary>Максимальное колличество символов в имени задачи.</summary>
    private const int TaskNameMaxLength = 20;

    /// <summary>Максимальное колличество символов в теле сообщения.</summary>
    private const int BodyMaxLength = 4070;

    /// <summary>Идентификатор чата.</summary>
    public long ChatId { get; set; }

    /// <summary>Идентификатор чата.</summary>
    public int MessageId { get; set; }

    /// <summary>Элементы сообщения.</summary>
    public SortedSet<TelegramMessageItem> Items { get; set; } = [];

    public override string ToString() => Items.FirstOrDefault() is not { } item ? string.Empty : new StringBuilder()
        .Append(!item.IsEnd ? "⏳" : string.Empty)
        .Append(item.IsError ? "❌" : "✅")
        .Append(' ')
        .AppendLineSubstring(item.TaskName, TaskNameMaxLength, defaultValue: "UNKNOWN")
        .AppendLine()
        .AppendSubstring(string.Join('\n', Items), BodyMaxLength)
        .ToString();
}
