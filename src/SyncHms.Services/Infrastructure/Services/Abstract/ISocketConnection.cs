namespace SyncHms.Services.Infrastructure.Services.Abstract;

public delegate void ConnectedHandle();

public delegate Task DisconnectedHandleAsync(Exception ex);

public delegate Task MessageHandleAsync(string message);

internal interface ISocketConnection
{
    event ConnectedHandle? ConnectedEvent;
    
    event DisconnectedHandleAsync? DisconnectedEvent;

    event MessageHandleAsync? MessageEvent;
    
    Task ConnectAsync();
    
    Task DisconnectAsync();

    Task SendAsync(string message);
}
