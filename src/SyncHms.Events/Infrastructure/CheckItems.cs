namespace SyncHms.Events.Infrastructure;

/// <summary>Структура, описывающая элементы формирования чека.</summary>
/// <param name="check">Чек.</param>
/// <param name="date">Дата.</param>
/// <param name="discount">Скидка.</param>
/// <param name="increase">Наценка.</param>
public readonly struct CheckItems(string check, string date, string discount, string increase)
{
    /// <summary>Чек.</summary>
    public string Check { get; } = check;

    /// <summary>Дата.</summary>
    public string Date { get; } = date;

    /// <summary>Скидка.</summary>
    public string Discount { get; } = discount;

    /// <summary>Наценка.</summary>
    public string Increase { get; } = increase;
}