namespace SyncHms.Events.Infrastructure;

/// <summary>Структура, описывающая элементы формирования чека.</summary>
/// <param name="check">Чек.</param>
/// <param name="date">Дата.</param>
/// <param name="discount">Скидка.</param>
internal readonly struct CheckItems(string check, string date, string discount)
{
    /// <summary>Чек.</summary>
    public string Check { get; } = check;

    /// <summary>Дата.</summary>
    public string Date { get; } = date;

    /// <summary>Скидка.</summary>
    public string Discount { get; } = discount;
}