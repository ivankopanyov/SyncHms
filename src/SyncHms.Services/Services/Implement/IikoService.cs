namespace SyncHms.Services.Services.Implement;

/// <summary>
/// Класс, описывающий сервис интеграции с удаленным сервисом <c>IIKO</c><br/>
/// Реализует интерфейс <see cref="IIikoService"/>
/// </summary>
/// <param name="control">Экземпляр контроллера, управляющего состоянием сервиса.</param>
internal class IikoService(IControl<IikoOptions, ApplicationEnvironment> control) : IIikoService
{
    public IReadOnlyDictionary<string, string> PayTypes => control.Options.PaymentTypes;

    public IikoConfiguration RouteConfiguration => control.Options.RouteConfiguration;
    
    /// <summary>Экземпляр окружения.</summary>
    public ApplicationEnvironment Environment => control.Environment;
    
    public async Task<string> LoginAsync()
    {
        try
        {
            var uri = GetUri($"resto/api/auth?login={control.Options.Login}&pass={control.Options.PasswordHash}");
            using var client = new HttpClient();
            using var response = await client.GetAsync(uri);
            var text = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode
                ? text
                : throw new HttpRequestException(text, null, response.StatusCode);
        }
        catch (Exception ex)
        {
            control.Unactive($"{ex.GetType().Name}, {ex.Message}");
            throw;
        }
    }
    
    public async Task LogoutAsync(string token)
    {
        try
        {
            var uri = GetUri($"resto/api/logout?key={token}");
            using var client = new HttpClient();
            using var response = await client.GetAsync(uri);
            var text = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(text, null, response.StatusCode);
        }
        catch (Exception ex)
        {
            control.Unactive($"{ex.GetType().Name}, {ex.Message}");
            throw;
        }
    }
    
    public async Task<List<PaymentTransaction>> GetPaymentTransactionsAsync(
        string token,
        DateOnly fromInclude,
        DateOnly toExclude,
        params OlapReportFilter[] filters)
    {
        try
        {
            var valueFilter = new ValuesFilter<string>(["NOT_DELETED"]);
            var requestFilters = new Dictionary<string, OlapReportFilter>
            {
                { "OpenDate.Typed", new DateRangeFilter(fromInclude, toExclude) },
                { "DeletedWithWriteoff", valueFilter },
                { "OrderDeleted", valueFilter }
            };

            foreach (var filter in filters)
                requestFilters.TryAdd(filter.Key, filter);
            
            var body = new OlapReportRequest
            {
                ReportType = "SALES",
                GroupByRowFields = [
                    "OrderNum",
                    "CloseTime",
                    "FiscalChequeNumber",
                    "RestaurantSection.Id",
                    "RestaurantSection",
                    "DishCategory.Id",
                    "DishCategory",
                    "DishId",
                    "DishName",
                    "DishAmountInt",
                    "PayTypes",
                    "PayTypes.GUID"
                ],
                AggregateFields = ["DishSumInt", "DiscountSum", "IncreaseSum", "DishReturnSum"],
                Filters = requestFilters
            };

            var json = JsonConvert.SerializeObject(body, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });
            
            var uri = GetUri($"resto/api/v2/reports/olap?key={token}");
            using var client = new HttpClient();
            using var response = await client.PostAsync(uri,
                new StringContent(json, Encoding.UTF8, "application/json"));
            
            if (!response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(text, null, response.StatusCode);
            }

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var jsonMessage = await new StreamReader(responseStream).ReadToEndAsync();
            return JsonConvert.DeserializeObject<PaymentTransactionRange>(jsonMessage)?.Data
                ?? throw new JsonSerializationException("Response is null");
        }
        catch (Exception ex)
        {
            control.Unactive($"{ex.GetType().Name}, {ex.Message}");
            throw;
        }
    }
    
    public async Task<HashSet<long>> GetOrderReturnedAsync(string token, DateTime fromInclude, DateTime toExclude)
    {
        try
        {
            var request = new IikoEventsRequest
            {
                Events = ["orderReturned"]
            };
            
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(IikoEventsRequest));
            await using var writer = new Utf8StringWriter();
            serializer.Serialize(writer, request);

            var fromDate = fromInclude.ToString("yyyy-MM-ddTHH:mm:ss.ffffff");
            var toDate = toExclude.ToString("yyyy-MM-ddTHH:mm:ss.ffffff");
            var uri = GetUri($"resto/api/events?key={token}&from_time={fromDate}&to_time={toDate}");
            using var client = new HttpClient();
            using var response = await client.PostAsync(uri,
                new StringContent(writer.ToString(), Encoding.UTF8, "application/xml"));
            
            if (!response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(text, null, response.StatusCode);
            }
            
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var xmlMessage = await new StreamReader(responseStream).ReadToEndAsync();
            
            var responseSerializer = new System.Xml.Serialization.XmlSerializer(typeof(IikoEventList));
            using var reader = new StringReader(xmlMessage);

            var numberFormatInfo = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };
            
            return responseSerializer.Deserialize(reader) is IikoEventList eventList
                ? eventList.Events
                    .Select(e => decimal.TryParse(e.Attributes.FirstOrDefault(a => a.Name == "orderNum")?.Value, numberFormatInfo, out var orderNum)
                        ? (long?)orderNum : -1)
                    .OfType<long>()
                    .ToHashSet()
                : throw new JsonSerializationException("Response is null");
        }
        catch (Exception ex)
        {
            control.Unactive($"{ex.GetType().Name}, {ex.Message}");
            throw;
        }
    }
    
    /// <summary>Метод, обрабатывающий изменение опций сервиса.</summary>
    /// <param name="options">Экземпляр опций сервиса.</param>
    public async Task ChangedOptionsHandleAsync(IikoOptions options)
    {
        var token = await LoginAsync();
        await LogoutAsync(token);
    }

    /// <summary>Метод, обрабатывающий изменение значений переменных окружения.</summary>
    /// <param name="current">Текущее окружение.</param>
    /// <param name="previous">Измененное окружение.</param>
    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current, ApplicationEnvironment previous) => Task.CompletedTask;

    private Uri GetUri(string url)
    {
        var baseUri = new Uri(control.Options.Url);
        return new Uri(baseUri, url);
    }
}
