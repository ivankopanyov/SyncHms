namespace SyncHms.Events.Messages;

/// <summary>Класс, описывающий базовую модель обновленного бронирования.</summary>
public abstract class GuestBase
{
    /// <summary>Номер бронирования.</summary>
    public long ReservationNumber { get; set; }
}
