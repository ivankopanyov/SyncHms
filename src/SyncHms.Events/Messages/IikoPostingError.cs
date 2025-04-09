namespace SyncHms.Events.Messages;

public class IikoPostingError
{
    public string Message { get; set; }

    public PaymentTransaction PaymentTransaction { get; set; }

    public override string ToString() => PaymentTransaction.ToString();
}