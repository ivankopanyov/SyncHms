namespace SyncHms.Bus.EntityFramework.Models;

internal abstract class BusEntity
{
    public abstract string Name { get; }

    public override int GetHashCode() => Name.GetHashCode();

    public override bool Equals(object? obj) =>
        obj is Exchange other && Name.Equals(other.Name);
    
    public static string GetName(Type type) =>
        type.AssemblyQualifiedName ?? type.FullName ?? type.Name;
}
