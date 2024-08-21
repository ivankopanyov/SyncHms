namespace SyncHms.Server.Options;

/// <summary>Класс, описывающий модель опций провайдера сервиса кеширования данных.</summary>
public class CacheProviderOptions
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "Cache";
    
    /// <summary>Тип провайдера.</summary>
    public CacheProvider Provider { get; set; }
}
