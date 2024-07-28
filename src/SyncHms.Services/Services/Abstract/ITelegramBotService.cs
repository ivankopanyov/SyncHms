namespace SyncHms.Services;

public interface ITelegramBotService : IService<TelegramBotOptions, ApplicationEnvironment>
{
    bool Enabled { get; }

    bool OnlyError { get; }

    IEnumerable<TelegramChat> Chats { get; }

    void Exec(Action<ITelegramBotClient> action);

    T Exec<T>(Func<ITelegramBotClient, T> func);
}
