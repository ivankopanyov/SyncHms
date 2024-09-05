namespace SyncHms.Events.Messages;

/// <summary>Класс, описывающий событие запроса номера комнаты бронирования из базы данных <c>OPERA</c>.</summary>
internal class RoomNumberRequest
{
    /// <summary>Идентификатор события из шины данных удаленного сервиса <c>Sanatorium</c></summary>
    public string CorrelationId { get; set; }
    
    /// <summary>Идентификатор профиля в сервисе <c>Sanatorium</c>.</summary>
    public string? ReservationGuestId { get; set; }
    
    /// <summary>Идентификатор фолио.</summary>
    public string? FolioGenericNo { get; set; }
    
    /// <summary>Номер чека.</summary>
    public string CheckNumber { get; set; }

    /// <summary>Позиции в чеке платежа.</summary>
    public IEnumerable<IEnumerable<FiscalCheckItem>> Checks { get; set; }
}
