namespace SyncHms.Events.Handlers.Telegram;

/// <summary>
/// Класс, описывающий обработчик события <see cref="ChangedServiceState"/>,
/// оповещающего об изменении состояния сервиса.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class ChangedServiceStateHandler(ITelegramBotService telegramBotService) : Handler<ChangedServiceState>
{
    /// <summary>Максимальное колличество символов в имени сервиса.</summary>
    private const int ServiceNameMaxLength = 50;
    
    /// <summary>Максимальное колличество символов в сообщении.</summary>
    private const int MessageMaxLength = 3000;
    
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="ChangedServiceState"/>.<br/>
    /// Отправляет информацию об изменении состояния сервиса в чаты, указанные в параметре окружения
    /// <see cref="ApplicationEnvironment.TelegramChats"/> бота <c>Telegram</c>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(ChangedServiceState @in, IEventContext context)
    {
        if (!telegramBotService.Enabled || !telegramBotService.Chats.Any())
            return;
        
        var stringBuilder = new StringBuilder()
            .AppendSubstring(@in.ServiceName, ServiceNameMaxLength, @in.Connection ? "✅ " : "❌ ", "Service")
            .Append($" {(@in.Connection ? string.Empty : "dis")}connected.");
        
        if (!string.IsNullOrWhiteSpace(@in.Message))
            stringBuilder
                .AppendLine()
                .AppendLine()
                .AppendSubstring(@in.Message, MessageMaxLength, @in.Connection ? "💬 " : "⚠️ ");
        
        var message = stringBuilder.ToString();
        
        foreach (var chat in telegramBotService.Chats)
            await telegramBotService.SendTextMessageAsync(chat.Id, message, chat.MessageThreadId);
    }
}
