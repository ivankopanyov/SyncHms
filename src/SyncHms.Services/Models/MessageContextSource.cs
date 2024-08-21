namespace SyncHms.Services.Models;

/// <summary>
/// Класс, описывающий объект, хранящий контекст обработки
/// сообщений из шины данных удаленного сервиса <c>Sanatorium</c>
/// </summary>
internal class MessageContextSource
{
    /// <summary>
    /// Экемпляр контекста обработки сообщений из шины данных удаленного сервиса <c>Sanatorium</c>
    /// </summary>
    public IMessageHandlerContext Context { get; set; }
    
    public CancellationTokenSource CancellationTokenSource { get; set; }
}
