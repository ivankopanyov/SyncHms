namespace SyncHms.Domain.EntityFramework;

/// <summary>Класс, описывающий модель опций домена на основе реляционной базы данных.</summary>
public class EntityFrameworkDomainOptions
{
    /// <summary>Свойство, содержащее объект настроек сериализатора <c>JSON</c>.</summary>
    internal JsonSerializerSettings JsonSerializerSettings { get; } = new();
    
    /// <summary>
    /// Свойство определяет, будут ли использоваться миграции при работе с базой данных.<br/>
    /// По умолчанию <c>true</c>.
    /// </summary>
    public bool UseMigrations { get; set; } = true;

    /// <summary>
    /// Свойство, которое позволяет настроить преобразователь контрактов для сериализации объектов в <c>JSON</c>.<br/>
    /// По умолчанию используется <see cref="DefaultContractResolver"/>.
    /// </summary>
    public IContractResolver ContractResolver
    {
        get => JsonSerializerSettings.ContractResolver ??= new DefaultContractResolver();
        set => JsonSerializerSettings.ContractResolver = value ?? new DefaultContractResolver();
    }
}
