namespace SyncHms.Services.Infrastructure.Services.Enums;

/// <summary>Перечисление возможных состояний подключения к сокету.</summary>
internal enum SocketState
{
    /// <summary>Ожидание подключения.</summary>
    Pending,
    
    /// <summary>Процесс установки подключения.</summary>
    Connecting,
    
    /// <summary>Подключение установлено.</summary>
    Connected,
    
    /// <summary>Подключение разорвано.</summary>
    Disconnected
}