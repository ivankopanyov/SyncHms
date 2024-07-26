namespace SyncHms.Fias.Entities.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal class FiasOptionsAttribute(Type type) : Attribute
{
    public Type Type { get; private set; } = type;
}
