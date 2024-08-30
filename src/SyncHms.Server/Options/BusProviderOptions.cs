namespace SyncHms.Server.Options;

/// <summary>Класс, описывающий модель опций провайдера шины данных.</summary>
public class BusProviderOptions
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "Bus";
    
    /// <summary>Тип провайдера.</summary>
    public BusProvider Provider { get; set; }
    
    /// <summary>Экземпляр опций провайдера реляционной базы данных.</summary>
    public SqlProviderOptions? Sql { get; set; }
}
