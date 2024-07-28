namespace SyncHms.Services.Services.Implement;

internal class FiscalService(IControl<CheckDbOptions, ApplicationEnvironment> control) : IFiscalService
{
    private CheckDBClient Client => new(CheckDBClient.EndpointConfiguration.BasicHttpBinding_ICheckDB, control.Options.Endpoint);

    public ApplicationEnvironment Environment => control.Environment;

    public void Exec(Action<CheckDBClient> action)
    {
        ArgumentNullException.ThrowIfNull(action, nameof(action));

        try
        {
            action.Invoke(Client);
            control.Active();
        }
        catch (Exception ex)
        {
            control.Unactive(ex);
            throw;
        }
    }

    public T Exec<T>(Func<CheckDBClient, T> func)
    {
        ArgumentNullException.ThrowIfNull(func, nameof(func));

        try
        {
            var result = func.Invoke(Client);
            control.Active();
            return result;
        }
        catch (Exception ex)
        {
            control.Unactive(ex);
            throw;
        }
    }

    public async Task ChangedOptionsHandleAsync(CheckDbOptions options)
    {
        var checkDbClient = new CheckDBClient(CheckDBClient.EndpointConfiguration.BasicHttpBinding_ICheckDB, options.Endpoint);
        await checkDbClient.GetCheckAsync(new Request());
    }

    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current, ApplicationEnvironment previous) => Task.CompletedTask;
}
