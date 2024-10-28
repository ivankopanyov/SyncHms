namespace SyncHms.Services.Services.Implement;

/// <summary>
/// Класс, описывающий сервис интеграции с удаленным сервисом <c>FIAS</c><br/>
/// Реализует интерфейс <see cref="IFiasService"/>
/// </summary>
/// <param name="control">Экземпляр контроллера, управляющего состоянием сервиса.</param>
/// <param name="cacheService">Экземпляр сервис кеширования данных.</param>
/// <param name="socketConnectionFactory">Экземпляр фабрики, создающей подключения к сокету.</param>
internal class FiasService(
    IControl<FiasServiceOptions, ApplicationEnvironment> control,
    IMemoryCache cacheService,
    ISocketConnectionFactory socketConnectionFactory)
    : IFiasService
{
    private readonly SemaphoreSlim _semaphore = new(1);

    /// <summary>
    /// Объект, блокирующийся при обновлении объекта <see cref="FiasService._cancellationTokenSource"/>
    /// </summary>
    private readonly object _lock = new();

    /// <summary>Флаг, указывающий была ли запущена проверка подключения к сервису <c>FIAS</c></summary>
    private bool _pipelineRunning;

    private CancellationTokenSource _cancellationTokenSource = new();

    /// <summary>Экземпляр объекта для подключения к сокету.</summary>
    private ISocketConnection? _socketConnection;
    
    /// <summary>
    /// Свойство, определяющее уникальный номер сообщения, по которому определяется ответное сообщение.
    /// </summary>
    private static int PostingSequenceNumber => int.Parse(DateTime.Now.ToString("HHmmssff"));

    /// <summary>Экземпляр окружения.</summary>
    public ApplicationEnvironment Environment => control.Environment;
    
    /// <summary>
    /// Событие, вызываемое при получении сервисом сообщения типа <see cref="FiasGuestCheckIn"/>,
    /// указывающее на изменение статуса бронирования на <c>CHECK_IN</c>
    /// </summary>
    public event FiasGuestCheckInHandle? FiasGuestCheckInEvent;
    
    /// <summary>
    /// Событие, вызываемое при получении сервисом сообщения типа <see cref="FiasGuestCheckOut"/>,
    /// указывающее на изменение статуса бронирования на <c>CHECK_OUT</c>
    /// </summary>
    public event FiasGuestCheckOutHandle? FiasGuestCheckOutEvent;
    
    /// <summary>
    /// Событие, вызываемое при получении сервисом сообщения типа <see cref="FiasGuestChange"/>,
    /// указывающее на изменение данных бронирования.
    /// </summary>
    public event FiasGuestChangeHandle? FiasGuestChangeEvent;

    /// <summary>
    /// Метод, отправляющий сообщение типа <see cref="FiasPostingSimple"/> 
    /// для синхронизации платежа с системой <c>OPERA</c>
    /// </summary>
    /// <param name="message">Тело сообщения.</param>
    /// <param name="timeoutSeconds">Время ожидания ответа в секундах.</param>
    /// <returns>Ответ от удаленного сервиса.</returns>
    /// <exception cref="InvalidOperationException">
    /// Исключение возбуждается, если сервис находится в остановленном состоянии, исходя из значения свойства
    /// <see cref="FiasServiceOptions.Enabled"/> в значении свойства
    /// <see cref="IControl{FiasServiceOptions, ApplicationEnvironment}.Options"/> параметра <c>control</c>,
    /// или нет соединения с удаленным сервисом.
    /// </exception>
    public async Task<FiasPostingAnswer> SendPostingAsync(FiasPostingSimple message, int timeoutSeconds = 60)
    {
        if (!control.Options.Enabled)
            throw new InvalidOperationException("Service is disabled.");
        
        if (_socketConnection == null)
            throw new InvalidOperationException("Service not connected.");
        
        var postingSequenceNumber = PostingSequenceNumber;
        message.PostingSequenceNumber = postingSequenceNumber;
        var cultureInfo = GetCultureInfo(control.Options.LocalizationCode);
        return await SendAsync<FiasPostingAnswer>(message.ToString(cultureInfo), postingSequenceNumber,
            timeoutSeconds, _socketConnection);
    }

    /// <summary>
    /// Метод, отправляющий сообщение типа <see cref="FiasPostingRequest"/> 
    /// для начисления платежа на комнату в системе <c>OPERA</c>
    /// </summary>
    /// <param name="message">Тело сообщения.</param>
    /// <param name="timeoutSeconds">Время ожидания ответа в секундах.</param>
    /// <returns>Ответ от удаленного сервиса.</returns>
    /// <exception cref="InvalidOperationException">
    /// Исключение возбуждается, если сервис находится в остановленном состоянии, исходя из значения свойства
    /// <see cref="FiasServiceOptions.Enabled"/> в значении свойства
    /// <see cref="IControl{FiasServiceOptions, ApplicationEnvironment}.Options"/> параметра <c>control</c>,
    /// или нет соединения с удаленным сервисом.
    /// </exception>
    /// <exception cref="Exception">
    /// Исключение возбуждается, если бронирование не было найдено или имеет статус <c>NoPost</c>
    /// </exception>
    public async Task<FiasPostingAnswer> SendPostingAsync(FiasPostingRequest message, int timeoutSeconds = 60)
    {
        var cultureInfo = GetCultureInfo(control.Options.LocalizationCode);

        if (!control.Options.Enabled)
            throw new InvalidOperationException("Service is disabled.");
        
        if (_socketConnection == null)
            throw new InvalidOperationException("Service not connected.");

        var postingSequenceNumber = PostingSequenceNumber;
        var request = new FiasPostingRequest
        {
            SalesOutlet = 1,
            PostingInquiry = message.RoomNumber,
            PostingSequenceNumber = postingSequenceNumber,
            DateTime = message.DateTime,
            PmsPaymentMethod = "ROOM",
            WorkStationId = "1",
            MaximumGuests = 10,
            UserId = "0"
        };

        try
        {
            var fiasPostingList = await SendAsync<List<FiasPostingList>>(request.ToString(cultureInfo), postingSequenceNumber,
                timeoutSeconds, _socketConnection);

            if (fiasPostingList.FirstOrDefault(l => l.ReservationNumber == message.ReservationNumber)
                is not { } posting)
                throw new KeyNotFoundException();

            if (posting.NoPostStatus == true)
                throw new Exception($"No Post status for reservation {message.ReservationNumber}.");
        }
        catch (KeyNotFoundException)
        {
            throw new Exception($"Reservation {message.ReservationNumber} not found.");
        }

        message.SalesOutlet = request.SalesOutlet;
        message.PmsPaymentMethod = request.PmsPaymentMethod;
        message.WorkStationId = request.WorkStationId;
        message.UserId = request.UserId;
        message.DateTime = DateTime.Now;
        message.PostingSequenceNumber = postingSequenceNumber;
        
        return await SendAsync<FiasPostingAnswer>(message.ToString(cultureInfo), postingSequenceNumber,
            timeoutSeconds, _socketConnection);
    }

    /// <summary>Метод, обрабатывающий изменение опций сервиса.</summary>
    /// <param name="options">Экземпляр опций сервиса.</param>
    /// <exception cref="Exception">
    /// Исключение возбуждается всегда.
    /// Служит для отправки контроллеру сервисов сообщения о инициализации нового подключения,
    /// или о том, что сервис находится в остановленном состоянии, исходя из значения свойства
    /// <see cref="FiasServiceOptions.Enabled"/> в значении свойства
    /// <see cref="IControl{FiasServiceOptions, ApplicationEnvironment}.Options"/> параметра <c>control</c>
    /// </exception>
    public Task ChangedOptionsHandleAsync(FiasServiceOptions options)
    {
        new Thread(ConnectAsync).Start(options);
        throw new Exception(options.Enabled
            ? "Restarting the service."
            : "Service is disabled");
    }

    /// <summary>Метод, обрабатывающий изменение значений переменных окружения.</summary>
    /// <param name="current">Текущее окружение.</param>
    /// <param name="previous">Измененное окружение.</param>
    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current,
        ApplicationEnvironment previous)
    {
        return Task.CompletedTask;
    }

    /// <summary>Метод отправки сообщений удаленному сервису <c>FIAS</c></summary>
    /// <typeparam name="T">Тип ожидаемого ответа.</typeparam>
    /// <param name="message">Тело сообщения.</param>
    /// <param name="key">Уникальный ключ сообщения.</param>
    /// <param name="timeout">Время ожидания ответа в секундах.</param>
    /// <param name="socketConnection">Экземпляр объекта подключения к сокету.</param>
    /// <returns>Ответное сообщение.</returns>
    /// <exception cref="TimeoutException">
    /// Исключение, возбуждаемое, если ответ не был получен за отведенное время.
    /// </exception>
    /// <exception cref="KeyNotFoundException">
    /// Исключение, возбуждаемое, если не было найдено исходное сообщение, на которое пришел ответ.
    /// </exception>
    private async Task<T> SendAsync<T>(string message, int key, int timeout,
        ISocketConnection socketConnection) where T : class
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var timeSpan = TimeSpan.FromSeconds(Math.Min(Math.Max(timeout, 0), 3600));
        var sequenceKey = key.ToString();
        await cacheService.PushAsync(sequenceKey, cancellationTokenSource, timeSpan);

        try
        {
            await socketConnection.SendAsync(message);
            await Task.Delay(timeSpan, cancellationTokenSource.Token);
            return await cacheService.PopAsync<T>(sequenceKey)
                ?? throw new TimeoutException($"No response from FIAS. Key: {key}.");
        }
        catch (TaskCanceledException)
        {
            return await cacheService.PopAsync<T>(sequenceKey)
                ?? throw new KeyNotFoundException($"Not found response from FIAS. Key: {key}.");
        }
    }

    /// <summary>Метод, устанавливающий подключение к удаленному сервису.</summary>
    /// <param name="fiasOptions">Экземпляр опций сервиса.</param>
    private async void ConnectAsync(object? fiasOptions)
    {
        var options = (FiasServiceOptions)fiasOptions!;
        await _semaphore.WaitAsync();

        if (!_pipelineRunning)
        {
            _pipelineRunning = true;
            new Thread(StartConnectionCheckingPipelineAsync).Start();
        }

        try
        {
            if (_socketConnection != null)
            {
                await _socketConnection.DisconnectAsync();
                _socketConnection = null;
            }

            if (!options.Enabled)
                return;
        
            _socketConnection = socketConnectionFactory.Create(
                options.Host,
                options.Port,
                FiasEnvironment.Head,
                FiasEnvironment.Tail);
        }
        finally
        {
            _semaphore.Release();
        }
        
        _socketConnection.MessageEvent += async message => await MessageHandleAsync(message, _socketConnection);
        _socketConnection.DisconnectedEvent += ex =>
        {
            control.Unactive(ex);
            new Thread(ConnectAsync).Start(options);
            return Task.CompletedTask;
        };

        await _socketConnection.ConnectAsync();
    }

    /// <summary>Метод, обрабатывающий полученное сообщение.</summary>
    /// <param name="message">Обрабатываемое сообщение.</param>
    /// <param name="socketConnection">Экземпляр объекта подключения к сокету.</param>
    private async Task MessageHandleAsync(string message, ISocketConnection socketConnection)
    {
        ContinueConnectionCheckingPipeline();

        List<FiasPostingList> fiasPostingList = [];
        foreach (var commonMessage in FiasCommonMessage.FromString(message))
        {
            switch (commonMessage.ToFiasMessageFromPmsObject())
            {
                case FiasLinkStart:
                    try
                    {
                        await SendOptionsAsync(socketConnection);
                    }
                    catch
                    {
                        // ignored
                    }

                    return;
                case FiasLinkAlive:
                    control.Active();
                    return;
                case FiasLinkEnd:
                    new Thread(ConnectAsync).Start(control.Options);
                    return;
                case FiasGuestCheckIn guestCheckIn:
                    FiasGuestCheckInEvent?.Invoke(guestCheckIn);
                    return;
                case FiasGuestCheckOut guestCheckOut:
                    FiasGuestCheckOutEvent?.Invoke(guestCheckOut);
                    return;
                case FiasGuestChange guestChange:
                    FiasGuestChangeEvent?.Invoke(guestChange);
                    return;
                case FiasPostingList postingList:
                    fiasPostingList.Add(postingList);
                    break;
                case FiasPostingAnswer postingAnswer:
                    await FiasPostingAnswerHandleAsync(postingAnswer);
                    return;
            }
        }

        await FiasPostingListHandleAsync(fiasPostingList);
    }

    /// <summary>Метод, отправляющий опции для определения моделей сообщений удаленному сервису.</summary>
    /// <param name="socketConnection">Экземпляр объекта подключения к сокету.</param>
    private async Task SendOptionsAsync(ISocketConnection socketConnection)
    {
        var cultureInfo = GetCultureInfo(control.Options.LocalizationCode);

        var linkDescription = new FiasLinkDescription
        {
            DateTime = DateTime.Now,
            VendorSystemsVersion = "1.0.3.0",
            InterfaceFamily = FiasInterfaceTypes.PayTv
        }.ToString(cultureInfo);

        await socketConnection.SendAsync(linkDescription);

        foreach (var fiasOptions in FiasConnectionOptions.Reservation)
            await socketConnection.SendAsync(new FiasLinkRecord(fiasOptions).ToString(cultureInfo));
        
        foreach (var fiasOptions in FiasConnectionOptions.Posting)
            await socketConnection.SendAsync(new FiasLinkRecord(fiasOptions).ToString(cultureInfo));

        var linkAlive = new FiasLinkAlive { DateTime = DateTime.Now }.ToString(cultureInfo);
        await socketConnection.SendAsync(linkAlive);
    }

    /// <summary>
    /// Метод, обрабатывающий коллекцию сообщений типа <see cref="FiasPostingList"/><br/>
    /// Возвращает ответ на сообщения типа <see cref="FiasPostingRequest"/> с указанным свойством
    /// <see cref="FiasPostingRequest.PostingInquiry"/>
    /// </summary>
    /// <param name="messages">Коллекция обрабатываемых сообщений.</param>
    private async Task FiasPostingListHandleAsync(List<FiasPostingList> messages)
    {
        var key = messages[0].PostingSequenceNumber.ToString();
        await cacheService.PushAsync(key, messages, TimeSpan.FromMinutes(1));
        var cancellationTokenSource = await cacheService.PopAsync<CancellationTokenSource>(key);
        if (cancellationTokenSource != null)
            await cancellationTokenSource.CancelAsync();
    }

    /// <summary>
    /// Метод, обрабатывающий сообщение типа <see cref="FiasPostingAnswer"/><br/>
    /// Возвращает ответ на сообщения типа <see cref="FiasPostingSimple"/> и <see cref="FiasPostingRequest"/>
    /// с нулевым свойством <see cref="FiasPostingRequest.PostingInquiry"/>
    /// </summary>
    /// <param name="message">Обрабатывемое сообщение.</param>
    private async Task FiasPostingAnswerHandleAsync(FiasPostingAnswer message)
    {
        var key = message.PostingSequenceNumber.ToString();
        await cacheService.PushAsync(key, message, TimeSpan.FromMinutes(1));
        var cancellationTokenSource = await cacheService.PopAsync<CancellationTokenSource>(key);
        if (cancellationTokenSource != null)
            await cancellationTokenSource.CancelAsync();
    }

    /// <summary>Метод проверки подключения к сервису <c>FIAS</c></summary>
    private async void StartConnectionCheckingPipelineAsync()
    {
        while (true)
        {
            CancellationToken token;

            lock (_lock)
            {
                token = _cancellationTokenSource.Token;
            }

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(10), token);
                if (_socketConnection == null)
                    continue;

                var linkAlive = new FiasLinkStart { DateTime = DateTime.Now }.ToString(GetCultureInfo(control.Options.LocalizationCode));

                try
                {
                    await _socketConnection.SendAsync(linkAlive);
                }
                catch
                {
                    new Thread(ConnectAsync).Start(control.Options);
                    continue;
                }

                await Task.Delay(TimeSpan.FromSeconds(30), token);

                new Thread(ConnectAsync).Start(control.Options);
            }
            catch
            {
                // ignored
            }
        }
    }

    /// <summary>
    /// Метод, вызываемый при получении сообщения от сервиса <c>FIAS</c><br/>
    /// Указывает на установленное соединение.
    /// </summary>
    private void ContinueConnectionCheckingPipeline()
    {
        lock (_lock)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new();
        }
    }

    /// <summary>Метод возвращает объект опций локализации.</summary>
    /// <param name="code">Код локализации.</param>
    /// <returns>Объект опций локализации.</returns>
    private static CultureInfo GetCultureInfo(string? code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return CultureInfo.CurrentCulture;

        try
        {
            return CultureInfo.GetCultureInfo(code);
        }
        catch
        {
            return CultureInfo.CurrentCulture;
        }
    }
}