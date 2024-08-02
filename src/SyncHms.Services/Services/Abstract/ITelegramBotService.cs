namespace SyncHms.Services;

public interface ITelegramBotService : IService<TelegramBotOptions, ApplicationEnvironment>
{
    bool Enabled { get; }

    bool OnlyError { get; }

    IEnumerable<TelegramChat> Chats { get; }

    Task<Message?> SendTextMessageAsync(long chatId, string message, int? messageThreadId);

    Task EditMessageTextAsync(long chatId, int messageId, string message);
}
