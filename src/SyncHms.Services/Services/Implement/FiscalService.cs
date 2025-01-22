namespace SyncHms.Services.Services.Implement;

/// <summary>
/// Класс, описывающий сервис интеграции с удаленным сервисом <c>MICROS</c><br/>
/// Реализует интерфейс <see cref="IFiscalService"/>
/// </summary>
/// <param name="control">Экземпляр контроллера, управляющего состоянием сервиса.</param>
internal class FiscalService(IControl<MicrosOptions, ApplicationEnvironment> control) : IFiscalService
{
    private static readonly SemaphoreSlim Semaphore = new(1);
    
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

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
    /// <returns>Идентификатор сохраненного чека.</returns>
    /// <exception cref="SerializationException">Не удалось серализовать фискальный чек.</exception>
    /// <exception cref="MessageDeserializationException">Не удалось десериализовать ответ от удаленного сервиса.</exception>
    /// <exception cref="InvalidOperationException">Не удалось сохранить чек.</exception>
    public async Task<int> SetCheckAsync(FiscalCheck fiscalCheck)
    {
        await Semaphore.WaitAsync();

        try
        {
            var cknumQuery = $"SELECT cknum FROM Checks WHERE rvc = {Environment.Rvc} ORDER BY date DESC LIMIT 1";
            var cknumResponse = await Client.ExecuteReaderAsync(cknumQuery);
            var cknum = int.TryParse(cknumResponse.ExecuteReaderResult?.Any1?.InnerText, out var num) ? num : 0;
            fiscalCheck.cknum = cknum <= 0 || ++cknum >= 10000 ? 1 : cknum;

            string json;

            try
            {
                json = System.Text.Json.JsonSerializer.Serialize(fiscalCheck, JsonSerializerOptions);
                if (json == null)
                    throw new SerializationException("Failed to serialize fiscal check");
            }
            catch (Exception ex)
            {
                throw new SerializationException("Failed to serialize fiscal check", ex);
            }

            var result = await Client.SetCheckJsonAsync(json);

            Response? response;

            try
            {
                response = System.Text.Json.JsonSerializer.Deserialize<Response>(result.SetCheckJsonResult);
                if (response == null)
                    throw new MessageDeserializationException("Could not get a response from Micros");
            }
            catch (Exception ex)
            {
                throw new MessageDeserializationException("Could not get a response from Micros", ex);
            }

            if (!response.success)
                throw new InvalidOperationException(response.errtext);

            control.Active();
            return cknum;
        }
        catch (Exception ex)
        {
            control.Unactive(ex);
            throw;
        }
        finally
        {
            Semaphore.Release();
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
