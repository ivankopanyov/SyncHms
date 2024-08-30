namespace SyncHms.Server.Options;

/// <summary>Класс, описывающий модель опций провайдера сервиса идентификации.</summary>
public class IdentityProviderOptions
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "Identity";
    
    /// <summary>Тип провайдера.</summary>
    public IdentityProvider Provider { get; set; }
    
    /// <summary>Экземпляр опций провайдера реляционной базы данных.</summary>
    public SqlProviderOptions? Sql { get; set; }
}
