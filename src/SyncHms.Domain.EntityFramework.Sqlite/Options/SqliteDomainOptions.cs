namespace SyncHms.Domain.EntityFramework.Sqlite;

/// <summary>
/// Класс, описывающий опции домена, основанного на <c>SQLite</c><br/>
/// Унаследован от класса <see cref="EntityFrameworkDomainOptions"/>
/// </summary>
public class SqliteDomainOptions : EntityFrameworkDomainOptions
{
    /// <summary>Строка подключения к базе данных <c>SQLite</c></summary>
    public string ConnectionString { get; set; }
}