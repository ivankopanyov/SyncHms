namespace SyncHms.Events.Messages;

/// <summary>Класс, описывающий событие запроса данных бронирования из базы данных <c>OPERA</c>.</summary>
internal class ReservationInfo
{
    /// <summary>Идентификатор бронирования.</summary>
    public decimal ReservationNumber { get; set; }

    /// <summary>Статус бронирования.</summary>
    public string Status { get; set; }

    /// <summary>Номер комнаты.</summary>
    public string? Room { get; set; }

    /// <summary>Дата и время заезда.</summary>
    public DateTime? ArrivalDate { get; set; }

    /// <summary>Дата и время выезда.</summary>
    public DateTime? DepartureDate { get; set; }

    /// <summary>
    /// Флаг, указывающий возможность начисления платежей на номер.<br/>
    /// Если значение <c>true</c> - функция начисления на номер отключена.
    /// </summary>
    public bool? NoPost {  get; set; }
}
