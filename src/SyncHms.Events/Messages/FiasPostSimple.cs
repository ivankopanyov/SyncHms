namespace SyncHms.Events.Messages;

/// <summary>
/// Класс, описывающий событие проведения денежного платежа.<br/>
/// Унаследован от класса <see cref="PostingBase"/>
/// </summary>
internal class FiasPostSimple : PostingBase
{
    /// <summary>
    /// Метод, приводящий объект к типу <see cref="CheckDetails"/><br/>
    /// Вызывает метод <see cref="PostingBase.Cast{T}"/>
    /// </summary>
    /// <returns>Объект типа <see cref="CheckDetails"/></returns>
    public CheckDetails ToCheckDetails() => Cast<CheckDetails>();
}
