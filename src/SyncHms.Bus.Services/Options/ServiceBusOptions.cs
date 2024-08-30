namespace SyncHms.Bus.Services;

/// <summary>Класс, описывающий модель опций сервисов приложения.</summary>
public class ServiceBusOptions
{
    /// <summary>Экземпляр конфигураций логгера.</summary>
    public LoggerConfiguration? LoggerConfiguration { get; set; }

    /// <summary>Имя файла для записи логов.</summary>
    public string? LogFileName { get; set; }
}
