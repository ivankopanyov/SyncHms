namespace SyncHms.Bus.Services;

/// <summary>Класс, описывающий атрибут, добавляющий описание свойств в процессе сериализации.</summary>
/// <param name="value">Описание свойства.</param>
[AttributeUsage(AttributeTargets.Property)]
public class DescriptionAttribute(string value) : Attribute
{
    /// <summary>Свойство, возвращающее описание свойства сериализуемого объекта.</summary>
    public string Value { get; private init; } = value;
}
