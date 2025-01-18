namespace SyncHms.Events.Messages;

/// <summary>
/// Класс, описывающий событие запроса номера комнаты бронирования из базы данных <c>OPERA</c><br/>
/// Унаследован от класса <see cref="PostingBase"/>
/// </summary>
internal class RoomNumberRequest : PostingBase
{
    /// <summary>Идентификатор профиля в сервисе <c>Sanatorium</c>.</summary>
    public string ReservationGuestId { get; set; }

    /// <summary>
    /// Метод, приводящий объект к типу <see cref="FiasPostRequest"/><br/>
    /// Вызывает метод <see cref="PostingBase.Cast{T}"/>
    /// </summary>
    /// <param name="reservationNumber">Идентификатор бронирования в базе данных <c>ОPERA</c></param>
    /// <param name="room">Номер комнаты.</param>
    /// <param name="profileNumber">Идентификатор профиля в базе данных <c>ОPERA</c></param>
    /// <returns>Объект типа <see cref="FiasPostRequest"/></returns>
    public FiasPostRequest ToFiasPostRequest(long reservationNumber, string room, long profileNumber)
    {
        var result = Cast<FiasPostRequest>();
        result.ReservationNumber = reservationNumber;
        result.Room = room;
        result.ProfileNumber = profileNumber;

        return result;
    }

    /// <summary>Переопределение метода <see cref="PostingBase.ToString"/></summary>
    public override string ToString() => $"{base.ToString()}, ReservationGuestId: {ReservationGuestId}";
}
