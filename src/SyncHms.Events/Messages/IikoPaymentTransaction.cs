namespace SyncHms.Events.Messages;

public class IikoPaymentTransaction
{
    /// <summary>Коллекция элементов чека с кодами локализации.</summary>
    private static readonly Dictionary<int, CheckItems> CheckItems = new()
    {
        { CultureInfo.InvariantCulture.LCID, new CheckItems("CHECK", "DATE", "Discount", "Increase") },
        { new CultureInfo("ru-RU").LCID, new CheckItems("ЧЕК", "ДАТА", "Скидка", "Наценка") }
    };

    public string CheckNumber { get; set; }
    
    public DateTime CloseDateTime { get; set; }

    public List<PaymentTransaction> Items { get; set; }

    /// <summary>Метод формирования чека.</summary>
    /// <param name="localizationCode">Код локализации чека.</param>
    /// <param name="header">Заголовок чека.</param>
    /// <returns>Сформированный чек.</returns>
    protected internal string ToCheck(string localizationCode, string? header = null)
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

        var checkPositions = Items
            .GroupBy(i => i.DishId)
            .Select(g => new PaymentTransaction
            {
                DishName = g.FirstOrDefault()?.DishName ?? string.Empty,
                DishAmountInt = g.Select(i => i.DishAmountInt).Sum(),
                DishSumInt = g.Select(i => i.DishSumInt).Sum(),
                DiscountSum = g.Select(i => -i.DiscountSum).Sum(),
                IncreaseSum = g.Select(i => i.IncreaseSum).Sum()
            })
            .ToList();

        List<PaymentTransaction> totals = [];
        
        var totalDiscount = Items.Select(i => -i.DiscountSum).Sum();
        var totalIncrease = Items.Select(i => i.IncreaseSum).Sum();
        
        if (totalDiscount != 0 || totalIncrease != 0)
            totals.Add(new PaymentTransaction
            {
                DishName = string.Empty,
                DishSumInt = Items.Select(i => i.DishSumInt).Sum()
            });
        
        if (totalDiscount != 0)
            totals.Add(new PaymentTransaction
            {
                DishName = checkItems.Discount,
                DishSumInt = totalDiscount
            });
        
        if (totalIncrease != 0)
            totals.Add(new PaymentTransaction
            {
                DishName = checkItems.Increase,
                DishSumInt = totalIncrease
            });
        
        var payTypeTotals = Items
            .GroupBy(i => i.PayTypeId)
            .Select(g => new PaymentTransaction
            {
                DishName = g.FirstOrDefault()?.PayTypeName ?? string.Empty,
                DishSumInt = g.Select(i => i.DishSumInt - i.DiscountSum + i.IncreaseSum).Sum()
            })
            .ToList();
        
        if (payTypeTotals.Count > 1)
            payTypeTotals.Add(new PaymentTransaction
            {
                DishName = string.Empty,
                DishSumInt = Items.Select(i => i.DishSumInt - i.DiscountSum + i.IncreaseSum).Sum()
            });

        List<PaymentTransaction> rows = [];
        rows.AddRange(checkPositions);
        rows.AddRange(totals);
        rows.AddRange(payTypeTotals);

        var amounts = new string[rows.Count];
        var sums = new string[rows.Count];
        var discounts = new string[rows.Count];
        var increases = new string[rows.Count];

        int amountMax = 0, totalMax = 0;

        for (var i = 0; i < rows.Count; i++)
        {
            amounts[i] = rows[i].DishAmountInt == 0 ? string.Empty : rows[i].DishAmountInt.ToString("0.###", invariantCulture);
            sums[i] = rows[i].DishSumInt.ToString("0.00", invariantCulture);
            discounts[i] = rows[i].DiscountSum == 0 ? string.Empty : rows[i].DiscountSum.ToString("0.00", invariantCulture);
            increases[i] = rows[i].IncreaseSum == 0 ? string.Empty : rows[i].IncreaseSum.ToString("0.00", invariantCulture);
            
            amountMax = Math.Max(amountMax, amounts[i].Length);
            totalMax = Math.Max(totalMax, Math.Max(increases[i].Length, Math.Max(discounts[i].Length, sums[i].Length)));
        }

        List<List<string>> items = [];
        var itemNameLength = lineLength - amountMax - totalMax - 2;

        foreach (var row in rows)
        {
            List<string> nameItems = [];

            for (var i = 0; i < row.DishName.Length; i += itemNameLength)
            {
                var to = Math.Min(i + itemNameLength, row.DishName.Length);
                nameItems.Add(row.DishName[i..to]);
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
                .Append(header.PadLeft(header.Length + Math.Max(0, (lineLength - header.Length) / 2)))
                .Append('\n')
                .Append(line)
                .Append('\n');
        }

        var checkNumberLimitLength = lineLength - checkItems.Check.Length - 1;
        var checkNumber = CheckNumber.Length <= checkNumberLimitLength
            ? CheckNumber
            : CheckNumber[..checkNumberLimitLength];

        stringBuilder
            .Append(checkItems.Check)
            .Append(checkNumber.PadLeft(lineLength - checkItems.Check.Length))
            .Append('\n')
            .Append(checkItems.Date)
            .Append(CloseDateTime.ToString("dd-MMM-yyyy HH:mm:ss", cultureInfo).PadLeft(lineLength - checkItems.Date.Length))
            .Append('\n')
            .Append(line)
            .Append('\n');

        int j = 0, n = checkPositions.Count;
        
        for (; j < n; j++)
            AddRowItem(stringBuilder, amounts[j], items[j], sums[j], discounts[j], increases[j],
                amountMax, totalMax, itemNameLength, checkItems.Discount, checkItems.Increase,
                j < checkPositions.Count - 1);

        if (totals.Count > 0)
        {
            n += totals.Count;
            stringBuilder.Append(line).Append('\n');
            for (; j < n; j++)
                AddRowItem(stringBuilder, amounts[j], items[j], sums[j], discounts[j], increases[j],
                    amountMax, totalMax, itemNameLength, checkItems.Discount, checkItems.Increase, false);
        }
        
        n += payTypeTotals.Count;
        stringBuilder.Append(line).Append('\n');
        for (; j < n; j++)
            AddRowItem(stringBuilder, amounts[j], items[j], sums[j], discounts[j], increases[j],
                amountMax, totalMax, itemNameLength, checkItems.Discount, checkItems.Increase, false);

        return stringBuilder.ToString();
    }

    public IikoPostings GetPostings(IReadOnlyDictionary<string, string> payTypes, IReadOnlyDictionary<string, int> restaurantSections,
        IReadOnlyList<string> categories, string? discountName, string? increaseName)
    {
        Dictionary<string, Dictionary<int, decimal[]>> postings = [];
        List<IikoPostingError> postingErrors = [];
        
        foreach (var item in Items)
        {
            if (!payTypes.TryGetValue(item.PayTypeName, out var operaPayType))
                continue;

            if (!restaurantSections.TryGetValue(item.RestaurantSectionName, out var salesOutlet))
            {
                postingErrors.Add(new IikoPostingError
                {
                    Message = $"Restaurant section name \"{item.RestaurantSectionName}\" not found in restaurant sections list.",
                    PaymentTransaction = item
                });
                
                continue;
            }

            var total = item.DishSumInt;
            
            if (discountName == null)
                total -= item.DiscountSum;
            else
                AddPosting(postings, postingErrors, categories, operaPayType, salesOutlet, discountName,
                    -item.DiscountSum, "Discount category name", item);
            
            if (increaseName == null)
                total += item.IncreaseSum;
            else
                AddPosting(postings, postingErrors, categories, operaPayType, salesOutlet, increaseName,
                    item.IncreaseSum, "Increase category name", item);

            var categoryName = item.DishCategoryName ?? string.Empty;
            AddPosting(postings, postingErrors, categories, operaPayType, salesOutlet, categoryName,
                total, "Category name", item);
        }
        
        var iikoPostings = new IikoPostings
        {
            CheckNumber = CheckNumber,
            DateTime = CloseDateTime,
            PostingErrors = postingErrors
        };

        foreach (var payType in postings)
        {
            foreach (var salesOutlet in payType.Value)
            {
                iikoPostings.Queue.Add(new IikoPosting
                {
                    PayType = payType.Key,
                    SalesOutlet = salesOutlet.Key,
                    Subtotals = salesOutlet.Value
                });
            }
        }

        return iikoPostings;
    }

    private static void AddPosting(Dictionary<string, Dictionary<int, decimal[]>> postings, List<IikoPostingError> postingErrors,
        IReadOnlyList<string> categories, string operaPayType, int salesOutlet, string categoryName, decimal sum,
        string categoryFieldName, PaymentTransaction paymentTransaction)
    {
        var transactionCodeIndex = GetTransactionCodeIndex(categories, categoryName);
        if (transactionCodeIndex is >= 0 and < 10)
        {
            if (sum == 0)
                return;
            
            if (!postings.TryGetValue(operaPayType, out var salesOutlets))
            {
                salesOutlets = [];
                postings.Add(operaPayType, salesOutlets);
            }
                
            if (!salesOutlets.TryGetValue(salesOutlet, out var transactionCodes))
            {
                transactionCodes = new decimal[10];
                salesOutlets.Add(salesOutlet, transactionCodes);
            }

            transactionCodes[transactionCodeIndex] += sum * 100;
        }
        else
        {
            postingErrors.Add(new IikoPostingError
            {
                Message = $"{categoryFieldName} \"{categoryName}\" not found in categories list.",
                PaymentTransaction = paymentTransaction
            });
        }
    }

    private static int GetTransactionCodeIndex(IReadOnlyList<string> categories, string categoryName)
    {
        for (var i = 0; i < Math.Max(10, categories.Count); i++)
        {
            if (categories[i] == categoryName)
                return i;
        }

        return -1;
    }

    private static void AddRowItem(StringBuilder stringBuilder, string amount, List<string> item, string total, string discount,
        string increase, int amountMax, int totalMax, int itemNameLength, string discountName, string increaseName, bool line)
    {
        stringBuilder
            .Append(amount.PadLeft(amountMax))
            .Append(' ')
            .Append((item.Count == 0 ? string.Empty : item[0]).PadRight(itemNameLength + 1))
            .Append(total.PadLeft(totalMax))
            .Append('\n');

        for (var j = 1; j < item.Count; j++)
            stringBuilder
                .Append(item[j].PadLeft(item[j].Length + amountMax + 1))
                .Append('\n');

        AddExtraRow(stringBuilder, discountName, discount, amountMax, totalMax, itemNameLength);
        AddExtraRow(stringBuilder, increaseName, increase, amountMax, totalMax, itemNameLength);

        if (line)
            stringBuilder.Append('\n');
    }

    private static void AddExtraRow(StringBuilder stringBuilder, string rowName, string rowValue,
        int amountMax, int totalMax, int itemNameLength)
    {
        if (rowValue.Length > 0)
        {
            stringBuilder
                .Append(rowName.PadLeft(amountMax + rowName.Length + 1).PadRight(itemNameLength + amountMax + 2))
                .Append(rowValue.PadLeft(totalMax))
                .Append('\n');
        }
    }
}