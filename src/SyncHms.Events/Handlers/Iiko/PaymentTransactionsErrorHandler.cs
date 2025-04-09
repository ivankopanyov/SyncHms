namespace SyncHms.Events.Handlers.Iiko;

public class PaymentTransactionsErrorHandler : Handler<PaymentTransactionsError>
{
    protected override Task HandleAsync(PaymentTransactionsError @in, IEventContext context)
    {
        var message = $"{@in.ExceptionTypeName}";
        if (@in.StatusCode != null)
            message += $" ({@in.StatusCode.ToString()})";

        message += $": {@in.Message}";
        
        context.Break(message);
        return Task.CompletedTask;
    }
}