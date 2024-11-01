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
    /// <returns>Данные бронирования.</returns>
    Task<ReservationUpdatedMessage?> GetReservationUpdatedMessageAsync(decimal reservationId, string status);

    /// <summary>Метод, возвращающий коллекцию бронирований, которые были обновлены в указанный период.</summary>
    /// <param name="fromDate">Минимальная дата обновления бронирования.</param>
    /// <param name="toDate">Максимальная дата обноаления бронирования.</param>
    /// <returns>Коллекция обновленных бронирований.</returns>
    Task<List<UpdatedReservation>> GetUpdatedReservationsAsync(DateTime fromDate, DateTime toDate);

    /// <summary>Метод, возвращающий номера бронирований профиля с указаным статусом.</summary>
    /// <param name="profileNumber">Номер профайла.</param>
    /// <param name="statuses">Статусы, которые должны быть установлены у бронирований профиля.</param>
    /// <returns>Список номеров бронирований.</returns>
    Task<List<decimal>> GetReservationsByProfileAsync(decimal profileNumber, ISet<string> statuses);
}
