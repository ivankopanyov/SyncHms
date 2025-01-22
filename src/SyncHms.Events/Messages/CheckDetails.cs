namespace SyncHms.Events.Messages;

/// <summary>
/// Класс, описывающий событие записи деталей чека в базу данных <c>OPERA</c><br/>
/// Унаследован от класса <see cref="PostingBase"/>
/// </summary>
internal class CheckDetails : PostingBase
{
    /// <summary>Коллекция элементов чека с кодами локализации.</summary>
    private static readonly IReadOnlyDictionary<int, CheckItems> CheckItems = new Dictionary<int, CheckItems>
    {
        { CultureInfo.InvariantCulture.LCID, new CheckItems("CHECK", "DATE", "Discount") },
        { new CultureInfo("ru-RU").LCID, new CheckItems("ЧЕК", "ДАТА", "Скидка") }
    };

    /// <summary>
    /// Метод, приводящий объект к типу <see cref="MicrosPostingRequest"/><br/>
    /// Вызывает метод <see cref="PostingBase.Cast{T}"/>
    /// </summary>
    /// <returns>Объект типа <see cref="MicrosPostingRequest"/></returns>
    public MicrosPostingRequest ToMicrosPostingRequest() => Cast<MicrosPostingRequest>();

    /// <summary>Метод формирования чека.</summary>
    /// <param name="localizationCode">Код локализации чека.</param>
    /// <param name="header">Заголовок чека.</param>
    /// <returns>Сформированный чек.</returns>
    public string ToCheck(string localizationCode, string? header = null)
    {
        CultureInfo cultureInfo;

        try
        {
            cultureInfo = CultureInfo.GetCultureInfo(localizationCode);
        }
        catch
        {
            cultureInfo = CultureInfo.InvariantCulture;
        }

        if (!CheckItems.TryGetValue(cultureInfo.LCID, out var checkItems))
        {
            cultureInfo = CultureInfo.InvariantCulture;
            checkItems = CheckItems[cultureInfo.LCID];
        }
        
        const int lineLength = 32;
        var invariantCulture = CultureInfo.InvariantCulture;

        var counts = new string[Transactions.Length];
        var amounts = new string[Transactions.Length];
        var discounts = new string[Transactions.Length];

        int countMax = 0, amountMax = 0;

        for (var i = 0; i < Transactions.Length; i++)
        {
            counts[i] = ((int)Transactions[i].Count).ToString();
            amounts[i] = Transactions[i].AmountBeforeDiscount.ToString("0.00", invariantCulture);
            discounts[i] = Transactions[i].AmountBeforeDiscount != 0 && Transactions[i].Amount != Transactions[i].AmountBeforeDiscount
                ? (Transactions[i].Amount - Transactions[i].AmountBeforeDiscount).ToString("0.00", invariantCulture)
                : string.Empty;

            countMax = Math.Max(countMax, counts[i].Length);
            amountMax = Math.Max(amountMax, Math.Max(discounts[i].Length, amounts[i].Length));
        }

        var totalString = Total.ToString("0.00", invariantCulture);
        amountMax = Math.Max(amountMax, totalString.Length);

        List<List<string>> items = [];
        var itemNameLength = lineLength - countMax - amountMax - 2;

        foreach (var transaction in Transactions)
        {
            List<string> nameItems = [];

            for (var i = 0; i < transaction.Name.Length; i += itemNameLength)
            {
                var to = Math.Min(i + itemNameLength, transaction.Name.Length);
                nameItems.Add(transaction.Name[i..to]);
            }

            items.Add(nameItems);
        }

        var line = string.Empty.PadLeft(lineLength, '-');
        header = header?.Trim();

        var stringBuilder = new StringBuilder();
        if (!string.IsNullOrEmpty(header))
        {
            if (header.Length > lineLength)
                header = header[..lineLength];
            
            stringBuilder
                .AppendLine(header.PadLeft(header.Length + Math.Max(0, (lineLength - header.Length) / 2)))
                .AppendLine(line);
        }

        var checkNumberLimitLength = lineLength - checkItems.Check.Length - 1;
        var checkNumber = CheckNumber.Length <= checkNumberLimitLength
            ? CheckNumber
            : CheckNumber[..checkNumberLimitLength];

        stringBuilder
            .Append(checkItems.Check)
            .AppendLine(checkNumber.PadLeft(lineLength - checkItems.Check.Length))
            .Append(checkItems.Date)
            .AppendLine(DateTime.ToString("dd-MMM-yyyy HH:mm:ss", cultureInfo).PadLeft(lineLength - checkItems.Date.Length))
            .AppendLine(line);

        for (var i = 0; i < Transactions.Length; i++)
        {
            stringBuilder
                .Append(counts[i].PadLeft(countMax))
                .Append(' ')
                .Append((items[i].Count == 0 ? string.Empty : items[i][0]).PadRight(itemNameLength + 1))
                .AppendLine(amounts[i].PadLeft(amountMax));

            for (var j = 1; j < items[i].Count; j++)
                stringBuilder.AppendLine(items[i][j].PadLeft(items[i][j].Length + countMax + 1));

            if (discounts[i].Length > 0)
            {
                stringBuilder
                    .Append(checkItems.Discount.PadLeft(countMax + checkItems.Discount.Length + 1).PadRight(itemNameLength + countMax + 2))
                    .AppendLine(discounts[i].PadLeft(amountMax));
            }

            stringBuilder.AppendLine();
        }

        var description = PaymentMethod.Length > itemNameLength ? PaymentMethod[..itemNameLength] : PaymentMethod;

        stringBuilder
            .Append(description.PadLeft(countMax + description.Length + 1).PadRight(itemNameLength + countMax + 2))
            .AppendLine(totalString.PadLeft(amountMax))
            .Append(line);

        return stringBuilder.ToString();
    }
}
