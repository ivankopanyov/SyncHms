namespace SyncHms.Server.Options;

/// <summary>Класс, описывающий модель опций провайдера домена.</summary>
public class DomainProviderOptions
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "Domain";
    
    /// <summary>Тип провайдера.</summary>
    public DomainProvider Provider { get; set; }
    
    /// <summary>Экземпляр опций провайдера реляционной базы данных.</summary>
    public SqlProviderOptions? Sql { get; set; }
}
