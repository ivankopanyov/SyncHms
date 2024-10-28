namespace SyncHms.Services;

/// <summary>Класс, описывающий модель обновленного бронирования.</summary>
public class UpdatedReservation
{
    /// <summary>Номер бронирования.</summary>
    public decimal ReservationNumber { get; set; }

    /// <summary>Статус бронирования.</summary>
    public string Status { get; set; }
}
