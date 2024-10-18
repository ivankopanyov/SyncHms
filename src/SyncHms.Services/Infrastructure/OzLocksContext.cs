namespace SyncHms.Services.Infrastructure;

/// <summary>
/// Класс, описывающий контекст подключения к базе данных <c>OPERA</c><br/>
/// Унаследован от класса <see cref="OzLocksContextBase"/>
/// </summary>
/// <param name="connectionString">Строка данных для подключения.</param>
internal class OzLocksContext(string connectionString) : OzLocksContextBase
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(connectionString);

}
