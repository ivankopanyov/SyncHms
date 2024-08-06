namespace SyncHms.Services;

public interface IFiscalService : IService<MicrosOptions, ApplicationEnvironment>
{
    Task<SetCheckResponse> SetCheckAsync(FiscalCheck fiscalCheck);
}
