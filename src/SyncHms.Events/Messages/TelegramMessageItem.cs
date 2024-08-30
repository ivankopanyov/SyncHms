namespace SyncHms.Events.Messages;

/// <summary>Класс, описывающий элемент сообщения, отправляемого ботом <c>Telegram</c></summary>
internal class TelegramMessageItem : IComparable<TelegramMessageItem>
{
    /// <summary>Максимальное колличество символов в описании события.</summary>
    private const int MaxLengthInfo = 100;

    /// <summary>Дата и время создания события.</summary>
    public DateTime DateTime { get; set; }

    /// <summary>Имя задачи.</summary>
    public string? TaskName { get; set; }

    /// <summary>Имя обработчика.</summary>
    public string? HandlerName { get; set; }

    /// <summary>Сообщение обрабатываемого события.</summary>
    public string? Message { get; set; }

    /// <summary>Наличие ошибки во время обработки события.</summary>
    public bool IsError { get; set; }

    /// <summary>Текст ошибки, вызванной вовремя обработки события.</summary>
    public string? Error { get; set; }

    /// <summary>Флаг, указывающий было ли завершено выполнение задачи.</summary>
    public bool IsEnd { get; set; }

    /// <summary>Метод, переопределяющий оператор неявного приведения типа
    /// <see cref="EventLog"/> к типу <see cref="TelegramMessageItem"/></summary>
    /// <param name="eventLog">
    /// Объект типа <see cref="EventLog"/>, приводимый к типу <see cref="TelegramMessageItem"/>
    /// </param>
    /// <returns>
    /// Объект типа <see cref="TelegramMessageItem"/>, приведеный из объекта типа <see cref="EventLog"/>
    /// </returns>
    public static implicit operator TelegramMessageItem(EventLog eventLog) => new()
    {
        DateTime = eventLog.DateTime,
        TaskName = eventLog.TaskName,
        HandlerName = eventLog.HandlerName,
        Message = eventLog.Message,
        IsError = eventLog.Error != null,
        Error = eventLog.Error,
        IsEnd = eventLog.IsEnd
    };

    /// <summary>
    /// Метод, реализующий метод интерфейса <see cref="IComparable{TelegramMessageItem}"/><br/>
    /// Определяет сортировку объектов типа <see cref="TelegramMessageItem"/> по параметру
    /// <see cref="DateTime"/> в обратном порядке.
    /// </summary>
    public int CompareTo(TelegramMessageItem? other) =>
        -DateTime.Compare(DateTime, other?.DateTime ?? DateTime.MinValue);

    public override string ToString() => new StringBuilder()
        .AppendLine($"🖥 {HandlerName ?? "UNKNOWN"}\n📅 {DateTime:dd.MM.yyyy HH:mm:ss}")
        .AppendLineSubstring(Message, MaxLengthInfo, "💬 ")
        .AppendLineSubstring(Error, MaxLengthInfo, "⚠️ ")
        .ToString();
}
