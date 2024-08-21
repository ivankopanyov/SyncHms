namespace SyncHms.Events.Extensions;

/// <summary>
/// Статический класс, который содержит методы расширения для класса <see cref="StringBuilder"/>
/// </summary>
internal static class StringBuilderExtensions
{
    /// <summary>Метод обрезает строку до максимального колличества символов и добавляет в
    /// <see cref="StringBuilder"/> с указанным префиксом.
    /// </summary>
    /// <param name="value">Обрабатываемая строка.</param>
    /// <param name="max">Максимальное колличество символов.</param>
    /// <param name="prefix">Префикс.</param>
    /// <param name="defaultValue">
    /// Значение по умолчанию, которое будет подставлено в случае, если переданная строка имеет значение <c>null</c>
    /// </param>
    public static StringBuilder AppendSubstring(this StringBuilder stringBuilder, string? value, int max,
        string? prefix = null, string? defaultValue = null) =>
        stringBuilder.Substring(value, max, false, prefix, defaultValue);

    /// <summary>Метод обрезает строку до максимального колличества символов, добавляет строку в
    /// <see cref="StringBuilder"/> с указанным префиксом и переносит курсор на новую строку.
    /// </summary>
    /// <param name="value">Обрабатываемая строка.</param>
    /// <param name="max">Максимальное колличество символов.</param>
    /// <param name="prefix">Префикс.</param>
    /// <param name="defaultValue">
    /// Значение по умолчанию, которое будет подставлено в случае, если переданная строка имеет значение <c>null</c>
    /// </param>
    public static StringBuilder AppendLineSubstring(this StringBuilder stringBuilder, string? value, int max,
        string? prefix = null, string? defaultValue = null) =>
        stringBuilder.Substring(value, max, true, prefix, defaultValue);

    /// <summary>Метод обрезает строку до максимального колличества символов и добавляет в
    /// <see cref="StringBuilder"/> с указанным префиксом.
    /// </summary>
    /// <param name="value">Обрабатываемая строка.</param>
    /// <param name="max">Максимальное колличество символов.</param>
    /// <param name="line">Флаг, указывающий нужно ли перенести курсор на новую строку после добавления.</param>
    /// <param name="prefix">Префикс.</param>
    /// <param name="defaultValue">
    /// Значение по умолчанию, которое будет подставлено в случае, если переданная строка имеет значение <c>null</c>
    /// </param>
    public static StringBuilder Substring(this StringBuilder stringBuilder, string? value, int max, bool line,
        string? prefix = null, string? defaultValue = null)
    {
        if (value != null || defaultValue != null)
        {
            stringBuilder
                .Append(prefix)
                .Append(Fix(value ?? defaultValue!, max));

            if (line)
                stringBuilder.AppendLine();
        }

        return stringBuilder;
    }

    /// <summary>Метод, обрезает переданную строку, если она содержит колличество символов больше указанного.</summary>
    /// <param name="value">Обрабатываемая строка.</param>
    /// <param name="max">Максимальное колличество символов.</param>
    /// <returns>Обработанная строка.</returns>
    private static string Fix(string value, int max) => value.Length > max ? $"{value[..(max - 3)]}..." : value;
}
