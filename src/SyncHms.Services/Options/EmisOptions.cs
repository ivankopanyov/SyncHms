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

    /// <summary>
    /// Статусы бронирваний, которые будут учитываться при синхронизации.
    /// <seealso cref="OperaReservationStatus"/>
    /// </summary>
    [Description("Статусы бронирваний, которые будут учитываться при синхронизации.")]
    [DictionaryEqual<string, bool>(
        OperaReservationStatus.Reserved,
        OperaReservationStatus.CheckedIn,
        OperaReservationStatus.CheckedOut,
        OperaReservationStatus.Cancelled,
        OperaReservationStatus.WaitList,
        OperaReservationStatus.NoShow,
        OperaReservationStatus.Prospect
    )]
    public Dictionary<string, bool> Statuses { get; set; }  = new()
    {
        { OperaReservationStatus.CheckedIn, false },
        { OperaReservationStatus.CheckedOut, false },
        { OperaReservationStatus.Reserved, false },
        { OperaReservationStatus.Cancelled, false },
        { OperaReservationStatus.NoShow, false },
        { OperaReservationStatus.WaitList, false },
        { OperaReservationStatus.Prospect, false }
    };

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
