namespace SyncHms.Events.Messages;

/// <summary>Класс, описывающий событие запроса данных бронирования из базы данных <c>OPERA</c>.</summary>
internal class ReservationInfo
{
    /// <summary>Идентификатор бронирования.</summary>
    public decimal ReservationNumber { get; set; }

    /// <summary>Статус бронирования.</summary>
    public string Status { get; set; }
}
