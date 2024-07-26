namespace SyncHms.Bus.Services;

[AttributeUsage(AttributeTargets.Property)]
public class DescriptionAttribute(string value) : Attribute
{
    public string Value { get; private init; } = value;
}
