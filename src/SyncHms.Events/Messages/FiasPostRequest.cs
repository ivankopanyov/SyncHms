namespace SyncHms.Events.Messages;

/// <summary>
/// Класс, описывающий событие начисления платежа на номер.<br/>
/// Унаследован от класса <see cref="PostingBase"/>
/// </summary>
internal class FiasPostRequest : PostingBase
{
    /// <summary>Идентификатор бронирования.</summary>
    public long ReservationNumber { get; set; }
    
    /// <summary>Идентификатор профиля.</summary>
    public long ProfileNumber { get; set; }
    
    /// <summary>Номер комнаты.</summary>
    public string Room { get; set; }

    /// <summary>
    /// Метод, приводящий объект к типу <see cref="CheckDetails"/><br/>
    /// Вызывает метод <see cref="PostingBase.Cast{T}"/>
    /// </summary>
    /// <returns>Объект типа <see cref="CheckDetails"/></returns>
    public CheckDetails ToCheckDetails() => Cast<CheckDetails>();

    /// <summary>Переопределение метода <see cref="PostingBase.ToString"/></summary>
    public override string ToString() => $"{base.ToString()}, Room: {Room}, Reservation: {ReservationNumber}";
}