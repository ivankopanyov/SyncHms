namespace SyncHms.Bus.Services;

/// <summary>
/// Класс, описывающий контракт сериализации и десериализации объектов,
/// включающих описание свойств с помощью <see cref="DescriptionAttribute"/><br/>
/// Унаследован от класса <see cref="DefaultContractResolver"/>
/// </summary>
public class DescriptionContractResolver : DefaultContractResolver
{
    /// <summary>
    /// Инициализация контракта.
    /// Задает свойству <see cref="NamingStrategy"/> значение <see cref="CamelCaseNamingStrategy"/>
    /// </summary>
    public DescriptionContractResolver() => NamingStrategy = new CamelCaseNamingStrategy();

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);
        if (member.GetCustomAttribute<DescriptionAttribute>() is { } attribute
            && !string.IsNullOrWhiteSpace(attribute.Value))
            property.PropertyName += $" ({attribute.Value})";

        return property;
    }

    protected override string ResolvePropertyName(string propertyName) =>
        base.ResolvePropertyName(propertyName).Split(" (")[0];
}
