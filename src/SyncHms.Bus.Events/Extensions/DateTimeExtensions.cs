namespace SyncHms.Bus.Events.Extensions;

/// <summary>Статический класс, содеращий методы расширения типа <see cref="DateTime"/></summary>
internal static class DateTimeExtensions
{
    /// <summary>Метод округляет значение до указанного колличества тактов.</summary>
    /// <param name="ticks">Колличество тактов для округления.</param>
    /// <returns>Округенное значение.</returns>
    public static DateTime Trim(this DateTime date, long ticks) =>
        new(date.Ticks - date.Ticks % ticks, date.Kind);
}
