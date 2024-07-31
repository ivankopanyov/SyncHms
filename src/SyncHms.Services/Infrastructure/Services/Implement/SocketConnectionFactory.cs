namespace SyncHms.Services.Infrastructure.Services.Implement;

internal class SocketConnectionFactory : ISocketConnectionFactory
{
    public ISocketConnection Create(string host, int port, char head, char tail,
        int connectionDelay = 6, int connectionTimeout = 1)
    {
        return new SocketConnection(host, port, head, tail, connectionDelay, connectionTimeout);
    }
}
