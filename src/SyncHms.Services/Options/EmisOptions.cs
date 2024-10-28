namespace SyncHms.Services.Options;

/// <summary>Класс, описывающий модель опций сервиса <c>EMIS</c></summary>
public class EmisOptions
{
    /// <summary>Имя секции в файле <c>appsettings.json</c></summary>
    public const string Section = "Emis";

    /// <summary>
    /// Флаг, определяющий разрешение на отправку сообщений,
    /// полученных от удаленного сервиса <c>EMIS</c> в шину данных.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>Задержка в секундах перед отправлением сообщения в шину данных.</summary>
    [Description("Задержка в секундах перед обновлением.")]
    [Range(0, int.MaxValue)]
    public int DelaySeconds { get; set; }

    public override int GetHashCode() => HashCode.Combine(Enabled);

    /// <summary>Метод, переопределяющий базовый метод <see cref="Equals"/></summary>
    /// <param name="obj">Объект для сравнения.</param>
    /// <returns>
    /// Если возвращается значение <c>false</c>,
    /// сервис инициирует переподключение к удаленному сервису с новыми опциями.
    /// </returns>
    public override bool Equals(object? obj) =>
        obj is EmisOptions other
        && Enabled == other.Enabled;
}
