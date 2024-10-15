namespace SyncHms.Services.Services.Implement;

/// <summary>
/// Класс, описывающий сервис интеграции с шиной данных <c>Sanatorium</c><br/>
/// Реализует интерфейс <see cref="ISanatoriumService"/>
/// </summary>
internal class SanatoriumService : ISanatoriumService
{
    /// <summary>Время задержки перед подключением к удаленному сервису.</summary>
    private static readonly TimeSpan ConnectionDelay = TimeSpan.FromSeconds(1);
    
    private readonly SemaphoreSlim _semaphore = new(1);

    /// <summary>Экземпляр контроллера, управляющего состоянием сервиса.</summary>
    private readonly IControl<SanatoriumOptions, ApplicationEnvironment> _control;

    /// <summary>Экземпляр сервиса кеширования данных.</summary>
    private readonly IMemoryCache _memoryCache;

    /// <summary>Экземпляр объекта подключения к шине данных <c>Sanatorium</c></summary>
    private IEndpointInstance? _endpointInstance;

    private CancellationTokenSource _cancellationTokenSource = new();

    private CancellationToken _cancellationToken;

    /// <summary>Экземпляр окружения.</summary>
    public ApplicationEnvironment Environment => _control.Environment;
    
    /// <summary>
    /// Событие, вызываемое при получении сервисом сообщения типа <see cref="PostingRequestEvent"/>,
    /// указывающее на совершение денежного платежа или начисления на комнату.
    /// </summary>
    public event PostingRequestHandle? PostingRequestEvent;

    /// <summary>Инициализация сервиса.</summary>
    /// <param name="control">Экземпляр контроллера, управляющего состоянием сервиса.</param>
    /// <param name="memoryCache">Экземпляр сервиса кеширования данных.</param>
    public SanatoriumService(IControl<SanatoriumOptions, ApplicationEnvironment> control, IMemoryCache memoryCache)
    {
        _control = control;
        _memoryCache = memoryCache;
        _cancellationToken = _cancellationTokenSource.Token;
    }
    
    /// <summary>Метод, обрабатывающий изменение опций сервиса.</summary>
    /// <param name="options">Экземпляр опций сервиса.</param>
    public Task ChangedOptionsHandleAsync(SanatoriumOptions options)
    {
        new Thread(Connect).Start(options);
        throw new Exception(options.Enabled
            ? "Restarting the service."
            : "Service is disabled");
    }
    
    /// <summary>Метод, обрабатывающий изменение значений переменных окружения.</summary>
    /// <param name="current">Текущее окружение.</param>
    /// <param name="previous">Измененное окружение.</param>
    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current,
        ApplicationEnvironment previous) => Task.CompletedTask;
    
    /// <summary>
    /// Метод, отправляющий сообщение типа <see cref="ReservationUpdatedMessage"/> 
    /// в шину данных <c>Sanatorium</c> для синхронизации данных бронирования.
    /// </summary>
    /// <param name="message">Тело сообщения.</param>
    /// <exception cref="InvalidOperationException">
    /// Искючение, возбуждаемое, если сервис находится в остановленном состоянии, исходя из значения свойства
    /// <see cref="SanatoriumOptions.Enabled"/> в значении свойства
    /// <see cref="IControl{FiasServiceOptions, ApplicationEnvironment}.Options"/> параметра <c>control</c>
    /// </exception>
    public async Task SendReservationUpdatedMessageAsync(ReservationUpdatedMessage message)
    {
        if (!_control.Options.Enabled)
            throw new InvalidOperationException("Service is disabled.");
        
        try
        {
            await _endpointInstance.Publish(message);
            _control.Active();
        }
        catch (Exception ex)
        {
            _control.Unactive(ex);
            throw;
        }
    }

    /// <summary>
    /// Метод, передающий сервису сообщение типа <see cref="PostTransactionsRequest"/>,
    /// полученное от обработчика <see cref="PostTransactionsRequestHandler"/>
    /// для вызова события <see cref="PostingRequestEvent"/>
    /// </summary>
    /// <param name="message">Тело сообщения.</param>
    /// <param name="context">Контекст обработки сообщения.</param>
    /// <param name="timeout">Таймаут в секундах на обработку сообщения и отправку ответа.</param>
    public async Task SendPostTransactionsRequestAsync(PostTransactionsRequest message,
        IMessageHandlerContext context, TimeSpan timeout)
    {
        if (!_control.Options.Enabled)
            return;
        
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        
        await _memoryCache.PushAsync(message.CorrelationId, new MessageContextSource
        {
            Context = context,
            CancellationTokenSource = cancellationTokenSource
        }, timeout);
        
        PostingRequestEvent?.Invoke(message);
        
        try
        {
            await Task.Delay(timeout, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // ignored
        }
    }

    /// <summary>
    /// Метод, отправляющий сообщение типа <see cref="PostTransactionsResponse"/> 
    /// в шину данных <c>Sanatorium</c> в качестве ответ на сообщение события <see cref="PostingRequestEvent"/>
    /// </summary>
    /// <param name="message">Тело сообщения.</param>
    /// <exception cref="InvalidOperationException">
    /// Искючение, возбуждаемое, если сервис находится в остановленном состоянии, исходя из значения свойства
    /// <see cref="SanatoriumOptions.Enabled"/> в значении свойства
    /// <see cref="IControl{FiasServiceOptions, ApplicationEnvironment}.Options"/> параметра <c>control</c>
    /// </exception>
    /// <exception cref="TimeoutException">
    /// Искючение, возбуждаемое, если сообщение не было обработано за отведенное время.
    /// </exception>
    public async Task SendPostTransactionsResponseAsync(PostTransactionsResponse message)
    {
        if (!_control.Options.Enabled)
            throw new InvalidOperationException("Service is disabled.");
        
        try
        {
            if (await _memoryCache.PopAsync<MessageContextSource>(message.CorrelationId) is not { } source)
                throw new TimeoutException("Response timed out.");
            
            await source.Context.Reply(message);
            _control.Active();
            await source.CancellationTokenSource.CancelAsync();
        }
        catch (Exception ex)
        {
            _control.Unactive(ex);
            throw;
        }
    }

    /// <summary>Метод, устанавливающий подключение к удаленному сервису.</summary>
    /// <param name="newOptions">Экземпляр опций сервиса.</param>
    private async void Connect(object? newOptions)
    {
        var options = (SanatoriumOptions)newOptions!;
        await _cancellationTokenSource.CancelAsync();

        await _semaphore.WaitAsync();

        try
        {
            if (_endpointInstance != null)
            {
                try
                {
                    await _endpointInstance.Stop();
                }
                catch
                {
                    // ignored
                }
            }

            if (!options.Enabled) return;

            if (_cancellationToken.IsCancellationRequested)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _cancellationToken = _cancellationTokenSource.Token;
            }

            await Task.Delay(ConnectionDelay, _cancellationToken);

            var endpointConfiguration = new EndpointConfiguration(options.Endpoint);
            endpointConfiguration.AssemblyScanner().ExcludeAssemblies("Logus.HMS.Messages");
            if (!string.IsNullOrEmpty(options.License))
                endpointConfiguration.License(options.License);
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration
                .UseTransport<SqlServerTransport>()
                .ConnectionString(options.ConnectionString)
                .EnableMessageDrivenPubSubCompatibilityMode();
            endpointConfiguration
                .UsePersistence<NHibernatePersistence>()
                .UseConfiguration(new Configuration
                {
                    Properties =
                    {
                        ["dialect"] = "NHibernate.Dialect.MsSql2008Dialect",
                        ["connection.connection_string"] = options.ConnectionString
                    }
                });
            endpointConfiguration.PurgeOnStartup(false);
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.RegisterComponents(services => services.RegisterSingleton<ISanatoriumService>(this));

            _endpointInstance = await Task.Run(async () => await Endpoint.Start(endpointConfiguration))
                .WaitAsync(_cancellationToken);

            _control.Active();
        }
        catch (OperationCanceledException ex)
        {
            _control.Unactive(_control.Options.Enabled
                ? ex
                : new Exception("Service is disabled"));
        }
        catch (Exception ex)
        {
            _control.Unactive(ex.Message.Split('\n')[0]);
            new Thread(Connect).Start(options);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
