namespace SyncHms.Services;

/// <summary>Статический класс, хранящий набор констант со статусами бронирования.</summary>
public static class ReservationStatus
{
    /// <summary>Забронировано.</summary>
    public const string Reserved = "RESERVED";

    /// <summary>Заехал.</summary>
    public const string CheckedIn = "CHECKED IN";

    /// <summary>Выехал.</summary>
    public const string CheckedOut = "CHECKED OUT";

    /// <summary>Отменено.</summary>
    public const string Cancelled = "CANCELLED";

    /// <summary>В списке ожидания.</summary>
    public const string WaitList = "WAITLIST";

    /// <summary>Не заехал.</summary>
    public const string NoShow = "NO SHOW";

    public const string Prospect = "PROSPECT";
}
