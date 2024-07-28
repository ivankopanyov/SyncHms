namespace SyncHms.Services.Services.Implement;

internal class FiasService : IFiasService
{
    private const char Head = FiasEnvironment.Head;

    private const char Tail = FiasEnvironment.Tail;

    private readonly IControl<FiasServiceOptions, ApplicationEnvironment> _control;

    private readonly IMemoryCache _cacheService;

    private readonly string _separator = $"{Tail}{Head}";

    private readonly object _lock = new();

    private DateTime _lastDateTime = DateTime.Now;

    private int _lastNumber;

    private Socket? _socket;

    private CancellationTokenSource _cancellationTokenSource = new();

    private CancellationToken _cancellationToken;

    public bool UseCheckDatabase => _control.Environment.UseCheckDatabase;

    public IDictionary<string, bool> TaxCodes => _control.Environment.TaxCodes;
    
    public event FiasGuestCheckInHandle? FiasGuestCheckInEvent;
    
    public event FiasGuestCheckOutHandle? FiasGuestCheckOutEvent;
    
    public event FiasGuestChangeHandle? FiasGuestChangeEvent;

    public FiasService(IControl<FiasServiceOptions, ApplicationEnvironment> control, IMemoryCache cacheService)
    {
        _control = control;
        _cacheService = cacheService;
        _cancellationToken = _cancellationTokenSource.Token;

        Connect();
    }

    public async Task<FiasPostingAnswer> SendPostingAsync(FiasPostingSimple message, int timeoutSeconds = 60)
    {
        var postingSequenceNumber = GetPostingSequenceNumber();
        message.PostingSequenceNumber = postingSequenceNumber;
        return await SendPostingAsync(message.ToString(), postingSequenceNumber, timeoutSeconds);
    }

    public async Task<FiasPostingAnswer> SendPostingAsync(FiasPostingRequest message, int timeoutSeconds = 60)
    {
        var postingSequenceNumber = GetPostingSequenceNumber();
        message.PostingSequenceNumber = postingSequenceNumber;
        return await SendPostingAsync(message.ToString(), postingSequenceNumber, timeoutSeconds);
    }

    public Task ChangedOptionsHandleAsync(FiasServiceOptions serviceOptions)
    {
        _cancellationTokenSource.Cancel();
        throw new Exception("Restarting the service.");
    }

    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current, ApplicationEnvironment previous)
    {
        if (current.UseReservation != previous.UseReservation || current.UsePosting != previous.UsePosting)
        {
            _cancellationTokenSource.Cancel();
            throw new Exception("Restarting the service.");
        }

        return Task.CompletedTask;
    }

    private async Task<FiasPostingAnswer> SendPostingAsync(string message, int postingSequenceNumber, int timeout)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var timeSpan = TimeSpan.FromSeconds(Math.Min(Math.Max(timeout, 0), 3600));
        var key = postingSequenceNumber.ToString();
        await _cacheService.PushAsync(key, cancellationTokenSource, timeSpan);

        try
        {
            await SendAsync(message);
            await Task.Delay(timeSpan, cancellationTokenSource.Token);
            return await _cacheService.PopAsync<FiasPostingAnswer>(key)
                ?? throw new TimeoutException($"No response from FIAS. PostingSequenceNumber: {key}.");
        }
        catch (TaskCanceledException)
        {
            return await _cacheService.PopAsync<FiasPostingAnswer>(key)
                ?? throw new KeyNotFoundException($"Not found response from FIAS. PostingSequenceNumber: {key}.");
        }
    }

    private void Connect() => new Thread(async () =>
    {
        if (_cancellationToken.IsCancellationRequested)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
        }

        try
        {
            if (_socket is { Connected: true })
            {
                var linkEnd = new FiasLinkEnd() { DateTime = DateTime.Now }.ToString();
                await SendAsync(linkEnd);
                _socket.Shutdown(SocketShutdown.Both);
                await _socket.DisconnectAsync(false, _cancellationToken);
            }

            _socket = null;

            await Task.Delay(TimeSpan.FromSeconds(Math.Max(_control.Options.ConnectionDelay, 0)), _cancellationToken);
            using Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ConnectToFias(socket);

            _socket = socket;
            _control.Active();

            StringBuilder stringBuilder = new();

            while (true)
                await ReadAsync(_socket, stringBuilder);
        }
        catch (Exception ex)
        {
            _control.Unactive(ex);
        }

        Connect();
    }).Start();

    private async Task ReadAsync(Socket socket, StringBuilder stringBuilder)
    {
        var buffer = new byte[8192];
        var size = await socket.ReceiveAsync(buffer, _cancellationToken);

        if (size == 0)
            return;

        var array = buffer.ToArray();
        if (size < array.Length)
            Array.Resize(ref array, size);

        var temp = Encoding.Default.GetString(array, 0, size);
        if (temp.Length == 0)
            return;

        var messages = temp.Split(_separator);

        stringBuilder.Append(messages[0]);

        if (messages[0].Length == 0 || messages[0][^1] == Tail || messages.Length > 1)
        {
            await MessageHandleAsync(stringBuilder.ToString());
            stringBuilder.Clear();
        }

        if (messages.Length == 1)
            return;

        if (messages.Length > 2)
            foreach (var message in messages[1..^1])
                await MessageHandleAsync(message);

        if (messages[^1].Length == 0)
            return;

        stringBuilder.Append(messages[^1]);

        if (messages[^1][^1] == Tail)
        {
            await MessageHandleAsync(stringBuilder.ToString());
            stringBuilder.Clear();
        }
    }

    private async Task MessageHandleAsync(string message)
    {
        var commonMessage = FiasCommonMessage.FromString(message);

        if (commonMessage.ToFiasMessageFromPmsObject() is { } fiasMessage)
        {
            if (fiasMessage.GetType() == typeof(FiasLinkStart))
            {
                var linkDescription = new FiasLinkDescription()
                {
                    DateTime = DateTime.Now,
                    VendorSystemsVersion = "1.0.3.0",
                    InterfaceFamily = FiasInterfaceTypes.PayTv
                }.ToString();

                await SendAsync(linkDescription);

                if (_control.Environment.UseReservation)
                    foreach (var fiasOptions in FiasConnectionOptions.Reservation)
                        await SendAsync(new FiasLinkRecord(fiasOptions).ToString());

                if (_control.Environment.UsePosting)
                    foreach (var fiasOptions in FiasConnectionOptions.Posting)
                        await SendAsync(new FiasLinkRecord(fiasOptions).ToString());

                var linkAlive = new FiasLinkAlive() { DateTime = DateTime.Now }.ToString();
                await SendAsync(linkAlive);
            }
            else if (fiasMessage.GetType() == typeof(FiasLinkEnd))
                await _cancellationTokenSource.CancelAsync();
            else switch (fiasMessage)
            {
                case FiasGuestCheckIn guestCheckIn:
                    FiasGuestCheckInEvent?.Invoke(guestCheckIn);
                    break;
                case FiasGuestCheckOut guestCheckOut:
                    FiasGuestCheckOutEvent?.Invoke(guestCheckOut);
                    break;
                case FiasGuestChange guestChange:
                    FiasGuestChangeEvent?.Invoke(guestChange);
                    break;
                case FiasPostingAnswer postingAnswer:
                {
                    var key = postingAnswer.PostingSequenceNumber.ToString();
                    await _cacheService.PushAsync(key, postingAnswer, TimeSpan.FromMinutes(1));
                    var cancellationTokenSource = await _cacheService.PopAsync<CancellationTokenSource>(key);
                    if (cancellationTokenSource != null)
                        await cancellationTokenSource.CancelAsync();
                    break;
                }
            }
        }
    }

    private async Task SendAsync(string message)
    {
        if (message is null)
            throw new ArgumentException("The message was not sent because it is null.");

        if (_socket is null)
            throw new InvalidOperationException("The message was not sent because the connection to FIAS was not established.");

        try
        {
            await _socket.SendAsync(Encoding.Default.GetBytes(message));
        }
        catch (Exception ex)
        {
            _control.Unactive(ex);
            await _cancellationTokenSource.CancelAsync();
            throw;
        }
    }

    private void ConnectToFias(Socket socket)
    {
        var addresses = Dns.GetHostAddresses(_control.Options.Host);
        if (addresses.Length == 0)
            throw new ArgumentException("Unable to retrieve address from specified host name.", nameof(_control.Options.Host));

        var ipEndPoint = new IPEndPoint(addresses[0], _control.Options.Port);

        if (!socket.ConnectAsync(ipEndPoint).Wait(TimeSpan.FromSeconds(Math.Max(_control.Options.ConnectionTimeout, 0)), _cancellationToken))
            throw new InvalidOperationException($"The remote host {_control.Options.Host}:{_control.Options.Port} was not found.");
    }

    private int GetPostingSequenceNumber()
    {
        lock (_lock)
        {
            var now = DateTime.Now;
            if (_lastDateTime != now)
            {
                _lastDateTime = now;
                _lastNumber = 0;
            }
            else if (_lastNumber > 99)
            {
                _lastNumber = 0;
            }

            return int.Parse($"{_lastDateTime:HHmmss}{_lastNumber++.ToString().PadLeft(2, '0')}");
        }
    }
}