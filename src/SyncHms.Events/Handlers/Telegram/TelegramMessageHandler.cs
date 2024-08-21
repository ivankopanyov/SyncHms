namespace SyncHms.Events.Handlers.Telegram;

/// <summary>
/// Класс, описывающий обработчик логов обработки событий.<br/>
/// Унаследован от класса <see cref="LogHandler"/>
/// </summary>
internal class TelegramMessageHandler(ITelegramBotService telegramBotService, ICache cache) : LogHandler
{
    private static readonly SemaphoreSlim Semaphore = new(1);

    /// <summary>
    /// Метод, обрабатывающий лог обработки события.<br/>
    /// Отправляет информацию о событии в чаты, указанные в параметре окружения
    /// <see cref="ApplicationEnvironment.TelegramChats"/> бота <c>Telegram</c>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр лога обработки события.</param>
    protected override async Task HandleAsync(EventLog @in)
    {
        if (!telegramBotService.Enabled || !telegramBotService.Chats.Any() || (@in.Error == null && telegramBotService.OnlyError))
            return;

        var taskId = @in.TaskId ?? "UNKNOWN";
        List<string> errors = [];

        foreach (var chat in telegramBotService.Chats)
        {
            if (@in.Error == null && chat.OnlyError)
                continue;

            var key = $"{chat.Id}/{chat.MessageThreadId}/{taskId}";

            await Semaphore.WaitAsync();

            try
            {
                if (await cache.GetAsync<TelegramMessage>(key) is { } message)
                {
                    message.Items.Add(@in);
                    await telegramBotService.EditMessageTextAsync(chat.Id, message.MessageId, message.ToString());
                }
                else
                {
                    message = new TelegramMessage
                    {
                        ChatId = chat.Id
                    };

                    message.Items.Add(@in);
                    var response = await telegramBotService.SendTextMessageAsync(chat.Id, message.ToString(), chat.MessageThreadId);
                    message.MessageId = response.MessageId;
                }

                if (@in.IsEnd)
                    await cache.PopAsync<TelegramMessage>(key);
                else
                    await cache.PushAsync(key, message);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
            finally
            {
                Semaphore.Release();
            }
        }

        if (errors.Count > 0)
            throw new Exception(string.Join(' ', errors));
    }
}
