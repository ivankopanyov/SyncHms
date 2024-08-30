namespace SyncHms.Bus.Events;

/// <summary>Класс, описывающий модель опций сервиса обработки событий.</summary>
public class EventBusOptions
{
    /// <summary>Экземпляр конфигураций логгера.</summary>
    public LoggerConfiguration? LoggerConfiguration { get; set; }

    /// <summary>Имя файла для записи логов.</summary>
    public string? LogFileName { get; set; }
}
