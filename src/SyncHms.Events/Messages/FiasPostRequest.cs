namespace SyncHms.Events.Messages;

/// <summary>Класс, описывающий событие начисления платежа на номер.</summary>
internal class FiasPostRequest
{
    /// <summary>Идентификатор события из шины данных удаленного сервиса <c>Sanatorium</c></summary>
    public string CorrelationId { get; set; }
    
    /// <summary>Идентификатор бронирования.</summary>
    public long ReservationNumber { get; set; }
    
    /// <summary>Идентификатор профиля.</summary>
    public long? ProfileNumber { get; set; }
    
    /// <summary>Номер комнаты.</summary>
    public string RoomNumber { get; set; }

    /// <summary>Номер чека.</summary>
    public string CheckNumber { get; set; }
    
    /// <summary>Позиции в чеке платежа.</summary>
    public IEnumerable<IEnumerable<FiscalCheckItem>> Checks { get; set; }
}
