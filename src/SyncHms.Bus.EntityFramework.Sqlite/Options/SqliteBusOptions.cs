namespace SyncHms.Bus.EntityFramework.Sqlite;

/// <summary>
/// Класс, описывающий опции шины данных, основанной на <c>SQLite</c><br/>
/// Унаследован от класса <see cref="EntityFrameworkBusOptions"/>
/// </summary>
public class SqliteBusOptions : EntityFrameworkBusOptions
{
    /// <summary>Строка подключения к базе данных <c>SQLite</c></summary>
    public string ConnectionString { get; set; }
}
