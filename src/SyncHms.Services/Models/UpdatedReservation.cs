namespace SyncHms.Services;

/// <summary>Класс, описывающий модель обновленного бронирования.</summary>
public class UpdatedReservation
{
    /// <summary>Номер бронирования.</summary>
    public decimal ReservationNumber { get; set; }

    /// <summary>Дата заезда.</summary>
    public DateTime? Arrival { get; set; }

    /// <summary>Дата выезда.</summary>
    public DateTime? Departure { get; set; }

    /// <summary>
    /// Статус бронирования.
    /// <seealso cref="ReservationStatus"/>
    /// </summary>
    public string Status { get; set; }
}
