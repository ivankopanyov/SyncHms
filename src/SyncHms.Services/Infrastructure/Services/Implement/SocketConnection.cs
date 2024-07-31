namespace SyncHms.Services.Infrastructure.Services.Implement;

internal class SocketConnection : ISocketConnection
{
    private readonly string _host;
    
    private readonly int _port;
    
    private readonly char _head;
    
    private readonly char _tail;
    
    private readonly string _separator;
    
    private readonly TimeSpan _connectionDelay;
    
    private readonly TimeSpan _connectionTimeout;
    
    private readonly CancellationTokenSource _cancellationTokenSource;

    private readonly CancellationToken _cancellationToken;

    private readonly object _lock = new();

    private Socket? _socket;

    private SocketState _state;
    
    public event ConnectedHandle? ConnectedEvent;
    
    public event DisconnectedHandleAsync? DisconnectedEvent;

    public event MessageHandleAsync? MessageEvent;

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

    public async Task DisconnectAsync()
    {
        await _cancellationTokenSource.CancelAsync();
    }

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
