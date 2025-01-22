namespace SyncHms.Events.Extensions;

/// <summary>
/// Статический класс, который содержит методы расширения для интерфейса <see cref="IEnumerable{T}"/>
/// </summary>
internal static class EnumerableExtensions
{
    /// <summary>Метод, распределяющий сумму платежа по кодам оплаты.</summary>
    /// <param name="transactions">Элементы платежа.</param>
    public static decimal[] Subtotals(this IEnumerable<string> list, TransactionDetails[] transactions)
    {
        return list.Select(item => transactions
            .Where(i => i.ServiceItemCode == item)
            .Select(i => i.Amount).Sum() * 100).ToArray();
    }
}
