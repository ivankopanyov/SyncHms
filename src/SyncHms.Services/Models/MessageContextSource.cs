namespace SyncHms.Services.Models;

internal class MessageContextSource
{
    public IMessageHandlerContext Context { get; set; }
    
    public CancellationTokenSource CancellationTokenSource { get; set; }
}
