namespace SyncHms.Services.Services.Implement;

internal class FiscalService(IControl<MicrosOptions, ApplicationEnvironment> control) : IFiscalService
{
    private CheckDBClient Client => new(CheckDBClient.EndpointConfiguration.BasicHttpBinding_ICheckDB, control.Options.Endpoint);

    public ApplicationEnvironment Environment => control.Environment;

    public async Task<SetCheckResponse> SetCheckAsync(FiscalCheck fiscalCheck)
    {
        try
        {
            var result = await Client.SetCheckAsync(fiscalCheck);
            control.Active();
            return result;
        }
        catch (Exception ex)
        {
            control.Unactive(ex);
            throw;
        }
    }

    public async Task ChangedOptionsHandleAsync(MicrosOptions options)
    {
        var checkDbClient = new CheckDBClient(CheckDBClient.EndpointConfiguration.BasicHttpBinding_ICheckDB, options.Endpoint);
        await checkDbClient.GetCheckAsync(new Request());
    }

    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current, ApplicationEnvironment previous) => Task.CompletedTask;
}
