namespace SyncHms.Services;

public interface IFiscalService : IService<CheckDbOptions, ApplicationEnvironment>
{
    Task<SetCheckResponse> SetCheckAsync(FiscalCheck fiscalCheck);
}
