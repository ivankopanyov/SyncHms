namespace SyncHms.Services.Infrastructure.Services.Abstract;

internal interface ISocketConnectionFactory
{
    ISocketConnection Create(string host, int port, char head, char tail,
        int connectionDelay = 6, int connectionTimeout = 1);
}
