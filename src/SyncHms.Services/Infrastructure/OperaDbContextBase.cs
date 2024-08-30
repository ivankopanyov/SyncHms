namespace SyncHms.Services;

/// <summary>
/// Абстрактный класс, описывающий базовый контекст подключения к базе данных <c>OPERA</c><br/>
/// Унаследован от класса <see cref="DbContext"/>
/// </summary>
public abstract class OperaDbContextBase : DbContext
{
    /// <summary>
    /// Множество типов, определенных в производном классе в свойствах типа <see cref="DbSet{TEntity}"/>.
    /// </summary>
    private static ISet<Type>? _types;

    /// <summary>
    /// Методы типов, хранящихся в поле <see cref="_types"/>,
    /// содержащие логику установки конфигураций модели в базе данных.
    /// </summary>
    private static IEnumerable<MethodInfo>? _methods;

    /// <summary>Инициализация контекста.</summary>
    protected internal OperaDbContextBase() { }

    /// <summary>Инициализация контекста.</summary>
    /// <param name="options">Опции подключения к базе данных.</param>
    protected internal OperaDbContextBase(DbContextOptions options) : base(options) { }

    /// <summary>
    /// Метод, переопределяющий базовый метод <see cref="DbContext.OnModelCreating"/><br/>
    /// Метод, с помощью инструментов рефлексии, инициализирует поле <see cref="_types"/> типами,
    /// определенными в производном классе в свойствах типа <see cref="DbSet{TEntity}"/>,
    /// поле <see cref="_methods"/> методами, хранящими логику конфигурации модели данного типа
    /// и вызывает эти методы.
    /// </summary>
    /// <param name="modelBuilder">Экземпляр построителя моделей.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("OPERA");

        _types ??= GetType().GetProperties()
            .Where(property => property.PropertyType.Name == "DbSet`1" &&
                               property.PropertyType.GenericTypeArguments.Length != 0)
            .Select(property => property.PropertyType.GenericTypeArguments.First())
            .ToHashSet();

        _methods ??= _types
            .SelectMany(type => type.GetMethods()
                .Where(method => method is { Name: nameof(OnModelCreating), IsStatic: true } &&
                                 method.GetParameters().Length == 2 &&
                                 method.GetParameters().Any(parameter => parameter.ParameterType == typeof(ModelBuilder)) &&
                                 method.GetParameters().Any(parameter => parameter.ParameterType == typeof(ISet<Type>))));

        foreach (var method in _methods)
            method.Invoke(null, new object[] { modelBuilder, _types });
    }
}
