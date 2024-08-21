namespace SyncHms.Services.Infrastructure.Services.Abstract;

/// <summary>Метод, вызываемый при установлении соединения с сокетом.</summary>
public delegate void ConnectedHandle();

/// <summary>Метод, вызываемый при при разрыве соединения с сокетом.</summary>
public delegate Task DisconnectedHandleAsync(Exception ex);

/// <summary>Метод, вызываемый при получении сообщения от сокета.</summary>
public delegate Task MessageHandleAsync(string message);

/// <summary>Интерфейс, определяющий методы подключения к сокету.</summary>
internal interface ISocketConnection
{
    /// <summary>Событие, вызываемое при установлении соединения с сокетом.</summary>
    event ConnectedHandle? ConnectedEvent;
    
    /// <summary>Событие, вызывемое при разрыве соединения с сокетом.</summary>
    event DisconnectedHandleAsync? DisconnectedEvent;

    /// <summary>Событие, вызываемое при получении сообщения от сокета.</summary>
    event MessageHandleAsync? MessageEvent;
    
    /// <summary>Метод, инициирующий попытку установления соединения с сокетом.</summary>
    Task ConnectAsync();
    
    /// <summary>Метод, инициирующий разрыв соединения с сокетом.</summary>
    Task DisconnectAsync();

    /// <summary>Метод, отправляющий сообщение сокету.</summary>
    /// <param name="message">Тело сообщения.</param>
    Task SendAsync(string message);
}
