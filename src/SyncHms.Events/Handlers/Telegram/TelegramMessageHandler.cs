﻿namespace SyncHms.Events.Handlers.Telegram;

public class TelegramMessageHandler(ITelegramBotService telegramBotService, ICache cache) : LogHandler
{
    private static readonly SemaphoreSlim _semaphore = new(1);

    protected override async Task HandleAsync(EventLog @in)
    {
        if (!telegramBotService.Enabled || !telegramBotService.Chats.Any() || (@in.Error == null && telegramBotService.OnlyError))
            return;

        await telegramBotService.Exec<Task>(async client =>
        {
            var taskId = @in.TaskId ?? "UNKNOWN";
            List<string> errors = [];

            foreach (var chat in telegramBotService.Chats)
            {
                if (@in.Error == null && chat.OnlyError)
                    continue;

                var key = $"{chat.Id}/{chat.MessageThreadId}/{taskId}";

                await _semaphore.WaitAsync();

                try
                {
                    if (await cache.GetAsync<TelegramMessage>(key) is { } message)
                    {
                        message.Items.Add(@in);
                        await client.EditMessageTextAsync(chat.Id, message.MessageId, message.ToString());
                    }
                    else
                    {
                        message = new TelegramMessage
                        {
                            ChatId = chat.Id
                        };

                        message.Items.Add(@in);
                        var response = await client.SendTextMessageAsync(chat.Id, message.ToString(), messageThreadId: chat.MessageThreadId);
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

                _semaphore.Release();
            }

            if (errors.Count > 0)
                throw new Exception(string.Join(' ', errors));
        });
    }
}
