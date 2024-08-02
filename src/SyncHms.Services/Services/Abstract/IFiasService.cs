namespace SyncHms.Services;

public delegate void FiasGuestCheckInHandle(FiasGuestCheckIn message);

public delegate void FiasGuestCheckOutHandle(FiasGuestCheckOut message);

public delegate void FiasGuestChangeHandle(FiasGuestChange message);

public interface IFiasService : IService<FiasServiceOptions, ApplicationEnvironment>
{
    event FiasGuestCheckInHandle? FiasGuestCheckInEvent;
    
    event FiasGuestCheckOutHandle? FiasGuestCheckOutEvent;
    
    event FiasGuestChangeHandle? FiasGuestChangeEvent;

    Task<FiasPostingAnswer> SendPostingAsync(FiasPostingSimple message, int timeoutSeconds = 60);

    Task<FiasPostingAnswer> SendPostingAsync(FiasPostingRequest message, int timeoutSeconds = 60);
}
