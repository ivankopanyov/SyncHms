namespace SyncHms.Fias.Entities.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
internal class FiasForAttribute(string name) : Attribute
{
    public string Name { get; private set; } = name;
}
