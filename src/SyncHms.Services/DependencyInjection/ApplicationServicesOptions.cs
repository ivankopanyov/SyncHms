namespace SyncHms.Services;

/// <summary>Класс, описывающий модель опций сервисов приложения.</summary>
public class ApplicationServicesOptions
{
    /// <summary>
    /// Экземпляр менеджера конфигураций приложения.<br/>
    /// Определяется для получения опций сервисов и значений переменных окружения
    /// из файла <c>appsettings.json</c> в случае отсутсвия в базе данных.
    /// </summary>
    public ConfigurationManager? ConfigurationManager { get; set; }
}
