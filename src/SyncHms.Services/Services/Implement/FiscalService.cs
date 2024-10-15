namespace SyncHms.Services.Services.Implement;

/// <summary>
/// Класс, описывающий сервис интеграции с удаленным сервисом <c>MICROS</c><br/>
/// Реализует интерфейс <see cref="IFiscalService"/>
/// </summary>
/// <param name="control">Экземпляр контроллера, управляющего состоянием сервиса.</param>
internal class FiscalService(IControl<MicrosOptions, ApplicationEnvironment> control) : IFiscalService
{
    /// <summary>
    /// Свойство, возвращающее новый экземпляр клиента для отправки сообщения удаленному сервису.
    /// </summary>
    private CheckDBClient Client => new(CheckDBClient.EndpointConfiguration.BasicHttpBinding_ICheckDB, control.Options.Endpoint);

    /// <summary>Экземпляр окружения.</summary>
    public ApplicationEnvironment Environment => control.Environment;

    /// <summary>
    /// Метод, отправляющий сообщение типа <see cref="FiscalCheck"/> 
    /// для сохранения чека в базе данных <c>MICROS</c>
    /// </summary>
    /// <param name="fiscalCheck">Объект чека.</param>
    /// <returns>Ответ от удаленного сервиса.</returns>
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

    /// <summary>Метод, обрабатывающий изменение опций сервиса.</summary>
    /// <param name="options">Экземпляр опций сервиса.</param>
    public async Task ChangedOptionsHandleAsync(MicrosOptions options)
    {
        var checkDbClient = new CheckDBClient(CheckDBClient.EndpointConfiguration.BasicHttpBinding_ICheckDB, options.Endpoint);
        await checkDbClient.GetCheckAsync(new Request());
    }
    
    /// <summary>Метод, обрабатывающий изменение значений переменных окружения.</summary>
    /// <param name="current">Текущее окружение.</param>
    /// <param name="previous">Измененное окружение.</param>
    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current, ApplicationEnvironment previous) => Task.CompletedTask;
}
