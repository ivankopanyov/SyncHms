namespace SyncHms.Bus.EntityFramework.Models;

/// <summary>Абстрактный класс, описывающий базовую сущность шины данных.</summary>
public abstract class BusEntity
{
    /// <summary>Имя сущности.</summary>
    public abstract string Name { get; }
    
    /// <summary>Статический метод, используемый для определения имени сущности шины данных.</summary>
    /// <param name="type">Тип сущности.</param>
    /// <returns>Имя сущности.</returns>
    public static string GetName(Type type) =>
        type.AssemblyQualifiedName ?? type.FullName ?? type.Name;
}
