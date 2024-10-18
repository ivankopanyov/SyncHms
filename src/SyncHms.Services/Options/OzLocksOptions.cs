/// <summary>
/// Класс, описывающий модель опций для установления подключения к базе данных <c>OzLocks</c>
/// </summary>
public class OzLocksOptions
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "OzLocks";

    /// <summary>Строка подключения к базе данных <c>OzLocks</c></summary>
    [Required(AllowEmptyStrings = true)]
    public string ConnectionString { get; set; } = string.Empty;

    public override int GetHashCode() => HashCode.Combine(ConnectionString);

    /// <summary>Метод, переопределяющий базовый метод <see cref="Equals"/></summary>
    /// <param name="obj">Объект для сравнения.</param>
    /// <returns>
    /// Если возвращается значение <c>false</c>,
    /// сервис инициирует переподключение к удаленному сервису с новыми опциями.
    /// </returns>
    public override bool Equals(object? obj) =>
        obj is OperaOptions other
        && ConnectionString == other.ConnectionString;
}
