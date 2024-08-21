namespace SyncHms.Domain.Converters;

/// <summary>
/// Класс, описывающий конвертер даты и времени для сериализации и десериализации в формат <c>JSON</c><br/>
/// Унаследован от класса <see cref="IsoDateTimeConverter"/>
/// </summary>
internal class DateTimeConverter : IsoDateTimeConverter
{
    /// <summary>
    /// Инициализация конвертера.
    /// Задает свойству <see cref="DateTimeConverter.DateTimeFormat"/> занчение <c>yyyy-MM-ddTHH:mm:ss.fffzzz</c>
    /// </summary>
    public DateTimeConverter() => DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffzzz";
}
