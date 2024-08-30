namespace SyncHms.Identity.Sqlite;

/// <summary>
/// Класс, описывающий опции сервисов идентификации пользователей, основанных на <c>SQLite</c><br/>
/// Унаследован от класса <see cref="IdentityOptions"/>
/// </summary>
public class SqliteIdentityOptions : IdentityOptions
{
    /// <summary>Строка подключения к базе данных <c>SQLite</c></summary>
    public string ConnectionString { get; set; }
}
