namespace SyncHms.Services.Services.Implement;

/// <summary>
/// Класс, описывающий сервис интеграции с удаленным сервисом API бота <c>Telegram</c><br/>
/// Реализует интерфейс <see cref="ITelegramBotService"/>
/// </summary>
internal class TelegramBotService : ITelegramBotService
{
    /// <summary>Экземпляр контроллера, управляющего состоянием сервиса.</summary>
    private readonly IControl<TelegramBotOptions, ApplicationEnvironment> _control;

    /// <summary>Чаты, в которые перенаправляются сообщения о результатах обработки событий.</summary>
    public IEnumerable<TelegramChat> Chats { get; private set; }
    
    /// <summary>
    /// Флаг, определяющий разрешение на попытки установления соединения с сервисом API бота <c>Telegram</c><br/>
    /// Определяется значением свойства <see cref="TelegramBotOptions.Enabled"/> в значении свойства
    /// <see cref="IControl{FiasServiceOptions, ApplicationEnvironment}.Options"/> параметра <c>control</c>
    /// </summary>
    public bool Enabled => _control.Options.Enabled;
    
    /// <summary>
    /// Флаг, указывающий, что все доступные чаты принимают только сообщения об ошибках в обработке событий.
    /// </summary>
    public bool OnlyError { get; private set; }
    
    /// <summary>Экземпляр окружения.</summary>
    public ApplicationEnvironment Environment => _control.Environment;

    /// <summary>Свойство, возвращающее экземпляр клиента для подключения к удаленному сервису.</summary>
    /// <exception cref="Exception">
    /// Искючение, возбуждаемое, если сервис находится в остановленном состоянии, исходя из значения свойства
    /// <see cref="TelegramBotOptions.Enabled"/> в значении свойства
    /// <see cref="IControl{FiasServiceOptions, ApplicationEnvironment}.Options"/> параметра <c>control</c>
    /// </exception>
    private TelegramBotClient Client => _control.Options.Enabled
        ? new TelegramBotClient(_control.Options.Token)
        : throw new Exception("Service is disabled.");

    /// <summary>Инициализация сервиса.</summary>
    /// <param name="control">Экземпляр контроллера, управляющего состоянием сервиса.</param>
    public TelegramBotService(IControl<TelegramBotOptions, ApplicationEnvironment> control)
    {
        _control = control;
        Chats = GetChats();
    }

    /// <summary>Метод, отправляющий сообщение в указанный чат.</summary>
    /// <param name="chatId">Идентификатор чата.</param>
    /// <param name="message">Тело сообщения.</param>
    /// <param name="messageThreadId">
    /// Идентификатор потока сообщений чата.<br/>
    /// Если передано значение <c>null</c> - сообщение будет отправлено в основной поток.
    /// </param>
    /// <returns>Экземпляр отправленного сообщения.</returns>
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
    
    /// <summary>Метод, изменяющий указанное сообщение в чате.</summary>
    /// <param name="chatId">Идентификатор чата.</param>
    /// <param name="messageId">Идентификатор сообщения.</param>
    /// <param name="message">Тело сообщения.</param>
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
    
    /// <summary>Метод, обрабатывающий изменение опций сервиса.</summary>
    /// <param name="options">Экземпляр опций сервиса.</param>
    public async Task ChangedOptionsHandleAsync(TelegramBotOptions options)
    {
        if (!_control.Options.Enabled)
            throw new Exception("Service is disabled.");

        if (!await Client.TestApiAsync())
            throw new Exception("API token is specified incorrectly.");
    }

    /// <summary>Метод, обрабатывающий изменение значений переменных окружения.</summary>
    /// <param name="current">Текущее окружение.</param>
    /// <param name="previous">Измененное окружение.</param>
    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current, ApplicationEnvironment previous)
    {
        Chats = GetChats();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Метод, возвращающий объекты чатов, исходя из значения свойства
    /// <see cref="ApplicationEnvironment.TelegramChats"/> в значении свойства
    /// <see cref="IControl{FiasServiceOptions, ApplicationEnvironment}.Environment"/> параметра <c>control</c>
    /// </summary>
    /// <returns>Объекты чатов.</returns>
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
