namespace SyncHms.Events.Messages;

/// <summary>
/// Класс, описывающий модель данных, хранящую информацию о бронировании
/// для запроса зарезервированного для него инвенторя из базы данных <c>OPERA</c>
/// </summary>
public class ReservationInventoryRequest
{
    /// <summary>Идентиыикатор бронирования.</summary>
    public decimal ReservationId { get; set; }

    /// <summary>Номер комнаты бронирования.</summary>
    public string? Room { get; set; }

    /// <summary>Привидение объекта типа <see cref="FiasGuestCheckIn"/> к типу <see cref="ReservationInventoryRequest"/></summary>
    public static implicit operator ReservationInventoryRequest(FiasGuestCheckIn checkIn)
    {
        return new ReservationInventoryRequest
        {
            ReservationId = checkIn.ReservationNumber,
            Room = checkIn.RoomNumber
        };
    }

    /// <summary>Привидение объекта типа <see cref="FiasGuestChange"/> к типу <see cref="ReservationInventoryRequest"/></summary>
    public static implicit operator ReservationInventoryRequest(FiasGuestChange change)
    {
        return new ReservationInventoryRequest
        {
            ReservationId = change.ReservationNumber,
            Room = change.RoomNumber
        };
    }

    /// <summary>Привидение объекта типа <see cref="FiasGuestCheckOut"/> к типу <see cref="ReservationInventoryRequest"/></summary>
    public static implicit operator ReservationInventoryRequest(FiasGuestCheckOut checkOut)
    {
        return new ReservationInventoryRequest
        {
            ReservationId = checkOut.ReservationNumber,
            Room = checkOut.RoomNumber
        };
    }
}
