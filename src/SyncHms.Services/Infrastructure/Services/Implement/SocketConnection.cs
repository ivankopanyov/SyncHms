namespace SyncHms.Services.Infrastructure.Services.Implement;

/// <summary>
/// Класс, описывающий объект подключения к сокету.<br/>
/// Реализует интерфейс <see cref="ISocketConnection"/>
/// </summary>
internal class SocketConnection : ISocketConnection
{
    /// <summary>Имя или адрес хоста для подключения.</summary>
    private readonly string _host;
    
    /// <summary>Номер порта для подключения.</summary>
    private readonly int _port;
    
    /// <summary>Символ, определяющий начало сообщения.</summary>
    private readonly char _head;
    
    /// <summary>Символ, определяющий конец сообщения.</summary>
    private readonly char _tail;
    
    /// <summary>
    /// Строка, определяющая место разделения сообщений.<br/>
    /// Образована в результате объединения значений полей
    /// <see cref="_head"/> и <see cref="_tail"/>
    /// </summary>
    private readonly string _separator;
    
    /// <summary>Время задержки перед попыткой усановления соединения с сокетом.</summary>
    private readonly TimeSpan _connectionDelay;
    
    /// <summary>Максимальное время для попытки усановления соединения с сокетом.</summary>
    private readonly TimeSpan _connectionTimeout;
    
    private readonly CancellationTokenSource _cancellationTokenSource;
    
    private readonly CancellationToken _cancellationToken;

    private readonly object _lock = new();

    /// <summary>Экземпляр сокета.</summary>
    private Socket? _socket;

    /// <summary>Текущее состояние соединения с сокетом.</summary>
    private SocketState _state;
    
    /// <summary>Событие, вызываемое при установлении соединения с сокетом.</summary>
    public event ConnectedHandle? ConnectedEvent;
    
    /// <summary>Событие, вызывемое при разрыве соединения с сокетом.</summary>
    public event DisconnectedHandleAsync? DisconnectedEvent;

    /// <summary>Событие, вызываемое при получении сообщения от сокета.</summary>
    public event MessageHandleAsync? MessageEvent;

    /// <summary>Инициализация объекта поключения к сокету.</summary>
    /// <param name="host">Имя или адрес хоста для подключения.</param>
    /// <param name="port">Номер порта для подключения.</param>
    /// <param name="head">Символ, определяющий начало сообщения.</param>
    /// <param name="tail">Символ, определяющий конец сообщения.</param>
    /// <param name="connectionDelay">
    /// Время задержки в секундах перед попыткой усановления соединения с сокетом.
    /// </param>
    /// <param name="connectionTimeout">
    /// Максимальное время в секундах для попытки усановления соединения с сокетом.
    /// </param>
    public SocketConnection(string host, int port, char head, char tail, int connectionDelay, int connectionTimeout)
    {
        _host = host;
        _port = port;
        _head = head;
        _tail = tail;
        _separator = $"{tail}{head}";
        _connectionDelay = TimeSpan.FromSeconds(Math.Max(connectionDelay, 0));
        _connectionTimeout = TimeSpan.FromSeconds(Math.Max(connectionTimeout, 0));
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
    }
    
    /// <summary>Метод, инициирующий попытку установления соединения с сокетом.</summary>
    /// <exception cref="ArgumentException">
    /// Исключение, возбуждаемое в случае, если указанный хост не был найден.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Исключение, возбуждаемое в случае, если не удалось установить соединение с сокетом.
    /// </exception>
    public async Task ConnectAsync()
    {
        lock (_lock)
        {
            if (_state != SocketState.Pending)
                return;

            _state = SocketState.Connecting;
        }
        
        try
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var addresses = await Dns.GetHostAddressesAsync(_host, _cancellationToken);
            if (addresses.Length == 0)
                throw new ArgumentException("Unable to retrieve address from specified host name.", nameof(_host));

            var ipEndPoint = new IPEndPoint(addresses[0], _port);
            await Task.Delay(_connectionDelay, _cancellationToken);
            if (!_socket.ConnectAsync(ipEndPoint).Wait(_connectionTimeout, _cancellationToken))
                throw new InvalidOperationException($"The remote host {_host}:{_port} was not found.");
        }
        catch (OperationCanceledException)
        {
            _state = SocketState.Disconnected;
            return;
        }
        catch (Exception ex)
        {
            _state = SocketState.Disconnected;
            DisconnectedEvent?.Invoke(ex);
            return;
        }
        
        _state = SocketState.Connected;
        ConnectedEvent?.Invoke();
        
        StringBuilder stringBuilder = new();

        new Thread(async () =>
        {
            try
            {
                while (true)
                    await ReadAsync(_socket, stringBuilder);
            }
            catch (OperationCanceledException)
            {
                _state = SocketState.Disconnected;
                await StopAsync();
            }
            catch (Exception ex)
            {
                _state = SocketState.Disconnected;
                await StopAsync();
                DisconnectedEvent?.Invoke(ex);
            }
        }).Start();
    }

    /// <summary>Метод, инициирующий разрыв соединения с сокетом.</summary>
    public async Task DisconnectAsync()
    {
        await _cancellationTokenSource.CancelAsync();
    }

    /// <summary>Метод, отправляющий сообщение сокету.</summary>
    /// <param name="message">Тело сообщения.</param>
    /// <exception cref="InvalidOperationException">
    /// Исключение, возбуждаемое в случае, если соединение с сокетом было разорвано.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Исключение, возбуждаемое в случае, если параметр <c>message</c> не инициализирован.
    /// </exception>
    public async Task SendAsync(string message)
    {
        if (_socket == null || _state != SocketState.Connected)
            throw new InvalidOperationException("Socket not connected");
        
        if (message == null)
            throw new ArgumentException("The message was not sent because it is null.");

        try
        {
            await _socket.SendAsync(Encoding.Default.GetBytes(message));
        }
        catch (Exception ex)
        {
            lock (_lock)
            {
                if (_state == SocketState.Disconnected)
                    return;

                _state = SocketState.Disconnected;
            }
            
            DisconnectedEvent?.Invoke(ex);
            await DisconnectAsync();
        }
    }

    /// <summary>Метод, инициирующий разрыв соединения с сокетом.</summary>
    private async Task StopAsync()
    {
        if (_socket == null)
            return;
        
        try
        {
            _socket.Shutdown(SocketShutdown.Both);
            await _socket.DisconnectAsync(false);
        }
        catch
        {
            // ignored
        }
    }
    
    /// <summary>Метод, обрабатывающий сообщения, полученные от сокета.</summary>
    /// <param name="socket">Экземпляр сокета.</param>
    /// <param name="stringBuilder">Экземпляр <see cref="StringBuilder"/> для записи сообщения.</param>
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

        if (messages[0].Length == 0 || messages[0][^1] == _tail || messages.Length > 1)
        {
            MessageEvent?.Invoke(stringBuilder.ToString());
            stringBuilder.Clear();
        }

        switch (messages.Length)
        {
            case 1:
                return;
            case > 2:
            {
                foreach (var message in messages[1..^1])
                    MessageEvent?.Invoke(message);
                break;
            }
        }

        if (messages[^1].Length == 0)
            return;

        stringBuilder.Append(messages[^1]);

        if (messages[^1][^1] == _tail)
        {
            MessageEvent?.Invoke(stringBuilder.ToString());
            stringBuilder.Clear();
        }
    }
}
