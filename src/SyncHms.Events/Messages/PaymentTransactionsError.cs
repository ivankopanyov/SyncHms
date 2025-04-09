namespace SyncHms.Events.Messages;

public class PaymentTransactionsError
{
    public string ExceptionTypeName { get; set; }

    public string Message { get; set; }

    public string? StackTrace { get; set; }

    public HttpStatusCode? StatusCode { get; set; }
    
    public PaymentTransactionsError() { }

    public PaymentTransactionsError(Exception ex, HttpStatusCode? statusCode = null)
    {
        ExceptionTypeName = ex.GetType().Name;
        Message = ex.Message;
        StackTrace = ex.StackTrace;
        StatusCode = statusCode;
    }
}
