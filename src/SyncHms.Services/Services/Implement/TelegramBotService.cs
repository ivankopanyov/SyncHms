namespace SyncHms.Services.Services.Implement;

internal class TelegramBotService : ITelegramBotService
{
    private readonly IControl<TelegramBotOptions, ApplicationEnvironment> _control;

    public IEnumerable<TelegramChat> Chats { get; private set; }

    public bool Enabled => _control.Options.Enabled;

    public bool OnlyError { get; private set; }

    public ApplicationEnvironment Environment => _control.Environment;

    private TelegramBotClient Client => _control.Options.Enabled
        ? new TelegramBotClient(_control.Options.Token)
        : throw new Exception("Service is disabled.");

    public TelegramBotService(IControl<TelegramBotOptions, ApplicationEnvironment> control)
    {
        _control = control;
        Chats = GetChats();
    }

    public async Task<Message?> SendTextMessageAsync(long chatId, string message, int? messageThreadId)
    {
        try
        {
            var result =  await Client.SendTextMessageAsync(chatId, message, messageThreadId: messageThreadId);
            _control.Active();
            return result;
        }
        catch (Exception ex)
        {
            _control.Unactive(ex);
            throw;
        }
    }

    public async Task EditMessageTextAsync(long chatId, int messageId, string message)
    {
        try
        {
            await Client.EditMessageTextAsync(chatId, messageId, message);
            _control.Active();
        }
        catch (Exception ex)
        {
            _control.Unactive(ex);
            throw;
        }
    }

    public async Task ChangedOptionsHandleAsync(TelegramBotOptions options)
    {
        if (!_control.Options.Enabled)
            throw new Exception("Service is disabled.");

        if (!await Client.TestApiAsync())
            throw new Exception("API token is specified incorrectly.");
    }

    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current, ApplicationEnvironment previous)
    {
        Chats = GetChats();
        return Task.CompletedTask;
    }

    private IEnumerable<TelegramChat> GetChats()
    {
        OnlyError = true;

        var result = _control.Environment.TelegramChats.Select(chat =>
        {
            var split = chat.Key.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (!long.TryParse(split[0], out var chatId))
                return null;

            int? messageThreadId = null;
            if (split.Length > 1)
            {
                if (!int.TryParse(split[1], out var threadId))
                    return null;

                messageThreadId = threadId;
            }

            if (!chat.Value)
                OnlyError = false;

            return new TelegramChat
            {
                Id = chatId,
                MessageThreadId = messageThreadId,
                OnlyError = chat.Value
            };
        }).Where(chat => chat != null);

        return result!;
    }
}
