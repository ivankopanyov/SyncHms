namespace SyncHms.Services;

/// <summary>Статический класс, хранящий набор констант со статусами бронирования системы <c>Sanatorium</c>.</summary>
public static class SanatoriumReservationStatus
{
    /// <summary>Забронировано.</summary>
    public const string Reserved = "RES";

    /// <summary>Заехал.</summary>
    public const string CheckedIn = "IN";

    /// <summary>Выехал.</summary>
    public const string CheckedOut = "OUT";

    /// <summary>Отменено.</summary>
    public const string Cancelled = "CANCEL";

    /// <summary>В списке ожидания.</summary>
    public const string WaitList = "WAIT";

    /// <summary>Не заехал.</summary>
    public const string NoShow = "NOSHOW";

    public const string TemporaryCheckedOut = "TMPOUT";
}
