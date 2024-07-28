namespace SyncHms.Events.Messages;

internal class TelegramMessage
{   
    private const int TaskNameMaxLength = 20;

    private const int BodyMaxLength = 4070;

    public long ChatId { get; set; }

    public int MessageId { get; set; }

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
