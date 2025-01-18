namespace SyncHms.Events.Extensions;

/// <summary>
/// Статический класс, который содержит методы расширения для класса <see cref="PostTransactionsRequest"/>
/// </summary>
internal static class PostTransactionsRequestExtensions
{
    /// <summary>Код транзакции, хранящей позиции платежа.</summary>
    private const string ItemsTransactionCode = "510";

    /// <summary>Код начисления на комнату.</summary>
    private const string RoomTransactionCode = "ROOM";

    /// <summary>Метод, приводящий объект к типу <see cref="RoomNumberRequest"/></summary>
    /// <param name="taxCodes">Платежные коды.</param>
    /// <returns>Объект типа <see cref="RoomNumberRequest"/></returns>
    /// <exception cref="ArgumentException">
    /// <see cref="PostTransactionsRequest.ReservationGuestId"/> не валидный.
    /// </exception>
    public static RoomNumberRequest ToRoomNumberRequest(this PostTransactionsRequest postTransactionsRequest, Dictionary<string,bool> taxCodes)
    {
        if (string.IsNullOrEmpty(postTransactionsRequest.ReservationGuestId))
            throw new ArgumentException("Medical Record external ID not specified.");

        if (!Regex.IsMatch(postTransactionsRequest.ReservationGuestId, @"^\d+/\d+$"))
            throw new ArgumentException($"Medical Record external ID {postTransactionsRequest.ReservationGuestId} not valid.");

        var transaction = postTransactionsRequest.MainTransaction(taxCodes);
        
        return new RoomNumberRequest
        {
            CorrelationId = postTransactionsRequest.CorrelationId,
            DateTime = postTransactionsRequest.PublishDate,
            TransactionCode = RoomTransactionCode,
            PaymentMethod = RoomTransactionCode,
            ReservationGuestId = postTransactionsRequest.ReservationGuestId,
            CheckNumber = transaction.Name,
            Total = Total(transaction),
            Transactions = transaction.Items
        };
    }

    /// <summary>Метод, приводящий объект к типу <see cref="FiasPostSimple"/></summary>
    /// <param name="taxCodes">Платежные коды.</param>
    /// <returns>Объект типа <see cref="FiasPostSimple"/></returns>
    /// <exception cref="ArgumentException">Код оплаты не определен.</exception>
    public static FiasPostSimple ToFiasPostSimple(this PostTransactionsRequest postTransactionsRequest, Dictionary<string,bool> taxCodes)
    {
        if (postTransactionsRequest.AmountTransaction() is not { } amountTransaction)
            throw new ArgumentException("Transaction code not specified.");
        
        var transaction = postTransactionsRequest.MainTransaction(taxCodes);
        
        return new FiasPostSimple
        {
            CorrelationId = postTransactionsRequest.CorrelationId,
            DateTime = postTransactionsRequest.PublishDate,
            TransactionCode = amountTransaction.TransactionCode,
            PaymentMethod = postTransactionsRequest.TransactionCode(),
            CheckNumber = transaction.Name,
            Total = Total(transaction),
            Transactions = transaction.Items
        };
    }

    /// <summary>Метод, приводящий объект к типу <see cref="MicrosPostingRequest"/></summary>
    /// <param name="taxCodes">Платежные коды.</param>
    /// <returns>Объект типа <see cref="MicrosPostingRequest"/></returns>
    public static MicrosPostingRequest ToMicrosPostingRequest(this PostTransactionsRequest postTransactionsRequest, Dictionary<string,bool> taxCodes)
    {
        var transaction = postTransactionsRequest.MainTransaction(taxCodes);
        
        var result = new MicrosPostingRequest
        {
            CorrelationId = postTransactionsRequest.CorrelationId,
            DateTime = postTransactionsRequest.PublishDate,
            CheckNumber = transaction.Name,
            Total = Total(transaction),
            Transactions = transaction.Items
        };

        if (postTransactionsRequest.AmountTransaction() is { } amountTransaction)
        {
            result.TransactionCode = amountTransaction.TransactionCode;
            result.PaymentMethod = postTransactionsRequest.TransactionCode();
        }
        else
        {
            result.TransactionCode = RoomTransactionCode;
            result.PaymentMethod = RoomTransactionCode;
        }

        return result;
    }

    /// <summary>Метод, определяющий, явдяется ли платеж начислением на комнату.</summary>
    /// <returns><c>true</c> - начисление на комнату.</returns>
    public static bool IsRoomPosting(this PostTransactionsRequest postTransactionsRequest)
    {
        return postTransactionsRequest.AmountTransaction() == null;
    }

    /// <summary>Метод, определяющий транзакцию, хранящую элементы платежа.</summary>
    /// <param name="taxCodes">Платежные коды.</param>
    /// <returns>Транзакция, хранящая элементы платежа.</returns>
    /// <exception cref="Exception">Транзакция не найдена или код оплаты не определен.</exception>
    private static PostTransactionRequest MainTransaction(this PostTransactionsRequest postTransactionsRequest, Dictionary<string,bool> taxCodes)
    {
        if (postTransactionsRequest.Transactions.FirstOrDefault(t => t.TransactionCode == ItemsTransactionCode) is not { } transaction)
            throw new Exception("Transaction not found.");

        if (transaction.Items.FirstOrDefault(i => !string.IsNullOrEmpty(i.ServiceItemCode) && !taxCodes.ContainsKey(i.ServiceItemCode)) is { } item)
            throw new Exception($"{item.Name} uses unsupported External ID {item.ServiceItemCode}.");

        return transaction;
    }

    /// <summary>Метод, определяющий транзакцию, хранящую код оплаты.</summary>
    /// <returns>Транзакция, хранящая код оплаты.</returns>
    private static PostTransactionRequest? AmountTransaction(this PostTransactionsRequest postTransactionsRequest)
    {
        return postTransactionsRequest.Transactions.FirstOrDefault(t => t.TransactionCode != ItemsTransactionCode && t.Items.Length == 1);
    }

    /// <summary>Метод, определяющий код оплаты.</summary>
    /// <returns>Код оплаты.</returns>
    private static string TransactionCode(this PostTransactionsRequest postTransactionsRequest)
    {
        return postTransactionsRequest.AmountTransaction()?.Items?.FirstOrDefault()?.Name ?? string.Empty;
    }

    /// <summary>Метод, вычисляющий общую сумму платежа.</summary>
    /// <returns>Общая сумма платежа.</returns>
    private static decimal Total(PostTransactionRequest transaction)
    {
        return transaction.Items.Select(i => i.Amount)?.Sum() ?? 0;
    }
}
