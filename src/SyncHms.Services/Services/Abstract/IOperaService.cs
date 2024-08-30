namespace SyncHms.Services;

/// <summary>
/// Интерфейс, определяющий методы сервиса интеграции с базой данных <c>OPERA</c><br/>
/// Расширяет методы интерфейса <see cref="IService{TOptions,TEnvironment}"/>
/// </summary>
public interface IOperaService : IService<OperaOptions, ApplicationEnvironment>
{
    /// <summary>Метод, запрашивающий номер комнаты бронирования в базе данных <c>OPERA</c></summary>
    /// <param name="reservationId">Идентификатор бронирования.</param>
    /// <returns>Номер комнаты бронирования.</returns>
    Task<string?> GetRoomNumberAsync(decimal reservationId);
    
    /// <summary>Метод, запрашивающий данные бронирования в базе данных <c>OPERA</c></summary>
    /// <param name="reservationId">Идентификатор бронирования.</param>
    /// <param name="status">Статус бронирования.</param>
    /// <param name="room">Номер комнаты бронирования.</param>
    /// <param name="arrival">Дата заезда.</param>
    /// <param name="departure">Дата выезда.</param>
    /// <param name="noPost">
    /// Флаг, указывающий на возможность начисления платежей на комнату.<br/>
    /// Если значения <c>true</c> - функция начисления на комнату отключена.
    /// </param>
    /// <returns>Данные бронирования.</returns>
    Task<ReservationUpdatedMessage?> GetReservationUpdatedMessageAsync(decimal reservationId, string status,
        string room, DateTime arrival, DateTime departure, bool? noPost);
}
