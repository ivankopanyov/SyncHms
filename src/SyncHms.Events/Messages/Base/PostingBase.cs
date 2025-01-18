namespace SyncHms.Events.Messages;

/// <summary>Абстрактный класс, описывающий событие проведения платежного начисления.</summary>
internal abstract class PostingBase
{
    /// <summary>Идентификатор события из шины данных удаленного сервиса <c>Sanatorium</c></summary>
    public string CorrelationId { get; set; }

    /// <summary>Дата и время совершения платежа.</summary>
    public DateTime DateTime { get; set; }

    /// <summary>Номер чека.</summary>
    public string CheckNumber { get; set; }

    /// <summary>Код оплаты.</summary>
    public string TransactionCode { get; set; }

    /// <summary>Метод оплаты.</summary>
    public string PaymentMethod { get; set; }

    /// <summary>Итоговая сумма платежа.</summary>
    public decimal Total { get; set; }

    /// <summary>Позиции в чеке.</summary>
    public TransactionDetails[] Transactions { get; set; }

    /// <summary>Метод, приводящий объект к типу, унаследованному от <see cref="PostingBase"/></summary>
    /// <typeparam name="T">Тип, к которому будет приведен объект.</typeparam>
    /// <returns>Приведенный объект.</returns>
    protected T Cast<T>() where T : PostingBase, new()
    {
        return new T
        {
            CorrelationId = CorrelationId,
            DateTime = DateTime,
            CheckNumber = CheckNumber,
            TransactionCode = TransactionCode,
            PaymentMethod = PaymentMethod,
            Total = Total,
            Transactions = Transactions
        };
    }

    /// <summary>Переопределение метода <see cref="object.ToString"/></summary>
    public override string ToString() =>
        $"Total: {Total.ToString("0.00", CultureInfo.InvariantCulture)}, CheckNumber: {CheckNumber}, TransactionCode: {TransactionCode}, Date: {DateTime:dd.MM.yyyy HH:mm:ss}";
}