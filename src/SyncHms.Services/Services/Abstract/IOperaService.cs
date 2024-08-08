namespace SyncHms.Services;

public interface IOperaService : IService<OperaOptions, ApplicationEnvironment>
{
    Task<string?> GetRoomNumberAsync(decimal reservationId);
    
    Task<ReservationUpdatedMessage?> GetReservationUpdatedMessageAsync(decimal reservationId, string status,
        string room, DateTime arrival, DateTime departure, bool? noPost);
}
