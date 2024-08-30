namespace SyncHms.Server.Options;

/// <summary>Класс, описывающий модель опций провайдера реляционной базы данных.</summary>
public class SqlProviderOptions
{
    /// <summary>Строка подключения к базе данных.</summary>
    public string? ConnectionString { get; set; }
}
