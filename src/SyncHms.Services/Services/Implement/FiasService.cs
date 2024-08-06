namespace SyncHms.Services.Services.Implement;

internal class FiasService(
    IControl<FiasServiceOptions, ApplicationEnvironment> control,
    IMemoryCache cacheService,
    ISocketConnectionFactory socketConnectionFactory)
    : IFiasService
{
    private readonly SemaphoreSlim _semaphore = new(1);
    
    private ISocketConnection? _socketConnection;
    
    private static int PostingSequenceNumber => int.Parse(DateTime.Now.ToString("HHmmssff"));

    public ApplicationEnvironment Environment => control.Environment;
    
    public event FiasGuestCheckInHandle? FiasGuestCheckInEvent;
    
    public event FiasGuestCheckOutHandle? FiasGuestCheckOutEvent;
    
    public event FiasGuestChangeHandle? FiasGuestChangeEvent;

    public async Task<FiasPostingAnswer> SendPostingAsync(FiasPostingSimple message, int timeoutSeconds = 90)
    {
        if (!control.Options.Enabled)
            throw new InvalidOperationException("Service is disabled.");
        
        if (_socketConnection == null)
            throw new InvalidOperationException("Service not connected.");
        
        var postingSequenceNumber = PostingSequenceNumber;
        message.PostingSequenceNumber = postingSequenceNumber;
        return await SendPostingAsync(message.ToString(), postingSequenceNumber,
            timeoutSeconds, _socketConnection);
    }

    public async Task<FiasPostingAnswer> SendPostingAsync(FiasPostingRequest message, int timeoutSeconds = 90)
    {
        if (!control.Options.Enabled)
            throw new InvalidOperationException("Service is disabled.");
        
        if (_socketConnection == null)
            throw new InvalidOperationException("Service not connected.");

        var postingSequenceNumber = PostingSequenceNumber;
        message.PostingSequenceNumber = postingSequenceNumber;
        return await SendPostingAsync(message.ToString(), postingSequenceNumber,
            timeoutSeconds, _socketConnection);
    }

    public Task ChangedOptionsHandleAsync(FiasServiceOptions options)
    {
        new Thread(ConnectAsync).Start(options);
        throw new Exception(options.Enabled
            ? "Restarting the service."
            : "Service is disabled");
    }

    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current,
        ApplicationEnvironment previous)
    {
        return Task.CompletedTask;
    }

    private async Task<FiasPostingAnswer> SendPostingAsync(string message,
        int postingSequenceNumber, int timeout, ISocketConnection socketConnection)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var timeSpan = TimeSpan.FromSeconds(Math.Min(Math.Max(timeout, 0), 3600));
        var key = postingSequenceNumber.ToString();
        await cacheService.PushAsync(key, cancellationTokenSource, timeSpan);

        try
        {
            await socketConnection.SendAsync(message);
            await Task.Delay(timeSpan, cancellationTokenSource.Token);
            return await cacheService.PopAsync<FiasPostingAnswer>(key)
                ?? throw new TimeoutException($"No response from FIAS. PostingSequenceNumber: {key}.");
        }
        catch (TaskCanceledException)
        {
            return await cacheService.PopAsync<FiasPostingAnswer>(key)
                ?? throw new KeyNotFoundException($"Not found response from FIAS. PostingSequenceNumber: {key}.");
        }
    }

    private async void ConnectAsync(object? fiasOptions)
    {
        var options = (FiasServiceOptions)fiasOptions!;
        await _semaphore.WaitAsync();

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
        
        _socketConnection.ConnectedEvent += control.Active;
        _socketConnection.MessageEvent += async message => await MessageHandleAsync(message, _socketConnection);
        _socketConnection.DisconnectedEvent += ex =>
        {
            control.Unactive(ex);
            new Thread(ConnectAsync).Start(options);
            return Task.CompletedTask;
        };

        await _socketConnection.ConnectAsync();
    }

    private async Task MessageHandleAsync(string message, ISocketConnection socketConnection)
    {
        var commonMessage = FiasCommonMessage.FromString(message);

        if (commonMessage.ToFiasMessageFromPmsObject() is { } fiasMessage)
        {
            switch (fiasMessage)
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

                    break;
                case FiasLinkEnd:
                    new Thread(ConnectAsync).Start(control.Options);
                    break;
                case FiasGuestCheckIn guestCheckIn:
                    FiasGuestCheckInEvent?.Invoke(guestCheckIn);
                    break;
                case FiasGuestCheckOut guestCheckOut:
                    FiasGuestCheckOutEvent?.Invoke(guestCheckOut);
                    break;
                case FiasGuestChange guestChange:
                    FiasGuestChangeEvent?.Invoke(guestChange);
                    break;
                case FiasPostingList postingList:
                    await FiasPostingListHandleAsync(postingList);
                    break;
                case FiasPostingAnswer postingAnswer:
                    await FiasPostingAnswerHandleAsync(postingAnswer);
                    break;
            }
        }
    }

    private async Task SendOptionsAsync(ISocketConnection socketConnection)
    {
        var linkDescription = new FiasLinkDescription
        {
            DateTime = DateTime.Now,
            VendorSystemsVersion = "1.0.3.0",
            InterfaceFamily = FiasInterfaceTypes.PayTv
        }.ToString();

        await socketConnection.SendAsync(linkDescription);

        foreach (var fiasOptions in FiasConnectionOptions.Reservation)
            await socketConnection.SendAsync(new FiasLinkRecord(fiasOptions).ToString());
        
        foreach (var fiasOptions in FiasConnectionOptions.Posting)
            await socketConnection.SendAsync(new FiasLinkRecord(fiasOptions).ToString());

        var linkAlive = new FiasLinkAlive { DateTime = DateTime.Now }.ToString();
        await socketConnection.SendAsync(linkAlive);
    }

    private async Task FiasPostingListHandleAsync(FiasPostingList message)
    {
        var key = message.PostingSequenceNumber.ToString();
        await cacheService.PushAsync(key, message, TimeSpan.FromMinutes(1));
        var cancellationTokenSource = await cacheService.PopAsync<CancellationTokenSource>(key);
        if (cancellationTokenSource != null)
            await cancellationTokenSource.CancelAsync();
    }

    private async Task FiasPostingAnswerHandleAsync(FiasPostingAnswer message)
    {
        var key = message.PostingSequenceNumber.ToString();
        await cacheService.PushAsync(key, message, TimeSpan.FromMinutes(1));
        var cancellationTokenSource = await cacheService.PopAsync<CancellationTokenSource>(key);
        if (cancellationTokenSource != null)
            await cancellationTokenSource.CancelAsync();
    }
}