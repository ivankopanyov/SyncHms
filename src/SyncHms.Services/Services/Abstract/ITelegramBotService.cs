namespace SyncHms.Services;

/// <summary>
/// Интерфейс, определяющий методы сервиса интеграции с удаленным сервисом API бота <c>Telegram</c><br/>
/// Расширяет методы интерфейса <see cref="IService{TOptions,TEnvironment}"/>
/// </summary>
public interface ITelegramBotService : IService<TelegramBotOptions, ApplicationEnvironment>
{
    /// <summary>
    /// Флаг, определяющий разрешение на попытки установления соединения с сервисом API бота <c>Telegram</c>
    /// </summary>
    bool Enabled { get; }

    /// <summary>
    /// Флаг, указывающий, что все доступные чаты принимают только сообщения об ошибках в обработке событий.
    /// </summary>
    bool OnlyError { get; }

    /// <summary>Чаты, в которые перенаправляются сообщения о результатах обработки событий.</summary>
    IEnumerable<TelegramChat> Chats { get; }

    /// <summary>Метод, отправляющий сообщение в указанный чат.</summary>
    /// <param name="chatId">Идентификатор чата.</param>
    /// <param name="message">Тело сообщения.</param>
    /// <param name="messageThreadId">
    /// Идентификатор потока сообщений чата.<br/>
    /// Если передано значение <c>null</c> - сообщение будет отправлено в основной поток.
    /// </param>
    /// <returns>Экземпляр отправленного сообщения.</returns>
    Task<Message?> SendTextMessageAsync(long chatId, string message, int? messageThreadId);

    /// <summary>Метод, изменяющий указанное сообщение в чате.</summary>
    /// <param name="chatId">Идентификатор чата.</param>
    /// <param name="messageId">Идентификатор сообщения.</param>
    /// <param name="message">Тело сообщения.</param>
    Task EditMessageTextAsync(long chatId, int messageId, string message);
}
