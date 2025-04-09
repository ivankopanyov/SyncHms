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
                DishSumInt = g.Select(i => i.DishSumInt + i.DishReturnSum).Sum(),
                DiscountSum = g.Select(i => i.DiscountSum).Sum(),
                IncreaseSum = g.Select(i => i.IncreaseSum).Sum()
            })
            .ToList();

        List<PaymentTransaction> totals = [];
        
        var totalDiscount = Items.Select(i => i.DiscountSum).Sum();
        if (totalDiscount != 0)
            totals.Add(new PaymentTransaction
            {
                DishName = checkItems.Discount,
                DishSumInt = Items.Select(i => i.DiscountSum).Sum()
            });
        
        var totalIncrease = Items.Select(i => i.IncreaseSum).Sum();
        if (totalIncrease != 0)
            totals.Add(new PaymentTransaction
            {
                DishName = checkItems.Increase,
                DishSumInt = Items.Select(i => i.IncreaseSum).Sum()
            });
        
        if (totals.Count > 0)
            totals.Add(new PaymentTransaction
            {
                DishName = string.Empty,
                DishSumInt = Items.Select(i => i.DishSumInt + i.DishReturnSum).Sum()
            });
        
        var payTypeTotals = Items
            .GroupBy(i => i.PayTypeId)
            .Select(g => new PaymentTransaction
            {
                DishName = g.FirstOrDefault()?.PayTypeName ?? string.Empty,
                DishSumInt = g.Select(i => i.DishSumInt - i.DiscountSum + i.IncreaseSum + i.DishReturnSum).Sum()
            })
            .ToList();
        
        if (payTypeTotals.Count > 1)
            payTypeTotals.Add(new PaymentTransaction
            {
                DishName = string.Empty,
                DishSumInt = Items.Select(i => i.DishSumInt - i.DiscountSum + i.IncreaseSum + i.DishReturnSum).Sum()
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

    public IikoPostings GetPostings(IikoConfiguration config, IReadOnlyDictionary<string, string> payTypes)
    {
        Dictionary<string, Dictionary<int, decimal[]>> postings = [];
        List<IikoPostingError> postingErrors = [];
        
        foreach (var item in Items)
        {
            if (!payTypes.TryGetValue(item.PayTypeName, out var operaPayType))
                continue;

            if (config.RestaurantSections?.FirstOrDefault(rs => rs.Name == item.RestaurantSectionName) is not { } restaurantSection)
            {
                postingErrors.Add(new IikoPostingError
                {
                    Message = $"Restaurant section name \"{item.RestaurantSectionName}\" not found in configuration.",
                    PaymentTransaction = item
                });
                
                continue;
            }

            if (restaurantSection.DishCategories?.FirstOrDefault(dc => dc.Name == item.DishCategoryName) is not { } dishCategory)
            {
                postingErrors.Add(new IikoPostingError
                {
                    Message = $"Dish category name \"{item.DishCategoryName}\" not found in configuration.",
                    PaymentTransaction = item
                });
                
                continue;
            }
            
            AddPosting(item, operaPayType, item.DishSumInt + item.DishReturnSum, postings, postingErrors, "Sum",
                config.TotalRoute,restaurantSection.TotalRoute, dishCategory.TotalRoute);
            
            if (item.DiscountSum != 0)
                AddPosting(item, operaPayType, item.DiscountSum, postings, postingErrors, "Discount", config.DiscountRoute,
                    restaurantSection.DiscountRoute, dishCategory.DiscountRoute);
            
            if (item.IncreaseSum != 0)
                AddPosting(item, operaPayType, item.IncreaseSum, postings, postingErrors, "Increase", config.IncreaseRoute,
                    restaurantSection.IncreaseRoute, dishCategory.IncreaseRoute);
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

    private static void AddPosting(PaymentTransaction paymentTransaction, string operaPayType, decimal sum,
        Dictionary<string, Dictionary<int, decimal[]>> postings, List<IikoPostingError> postingErrors,
        string postingName, params int?[]?[] sourceRoutes)
    {
        int?[] route = [null, null];

        foreach (var sourceRoute in sourceRoutes)
        {
            if (sourceRoute == null)
                continue;
            
            for (var i = 0; i < Math.Min(sourceRoute.Length, route.Length); i++)
                if (sourceRoute[i] != null)
                    route[i] = sourceRoute[i];
        }
        
        if (route[0] is { } salesOutletNumber)
        {
            if (route[1] is { } transactionCodeIndex)
            {
                if (transactionCodeIndex is >= 1 and <= 10)
                {
                    transactionCodeIndex--;
                        
                    if (!postings.TryGetValue(operaPayType, out var salesOutlet))
                    {
                        salesOutlet = [];
                        postings.Add(operaPayType, salesOutlet);
                    }

                    if (!salesOutlet.TryGetValue(salesOutletNumber, out var transactionCodes))
                    {
                        transactionCodes = new decimal[10];
                        salesOutlet.Add(salesOutletNumber, transactionCodes);
                    }

                    transactionCodes[transactionCodeIndex] += sum * 100;
                }
                else
                {
                    postingErrors.Add(new IikoPostingError
                    {
                        Message = $"{postingName} post failed. Transaction code index \"{transactionCodeIndex}\" out of range bounds [1..10].",
                        PaymentTransaction = paymentTransaction
                    });
                }
            }
            else
            {
                postingErrors.Add(new IikoPostingError
                {
                    Message = $"{postingName} post failed. Transaction code index route not found.",
                    PaymentTransaction = paymentTransaction
                });
            }
        }
        else
        {
            postingErrors.Add(new IikoPostingError
            {
                Message = $"{postingName} post failed. Sales outlet number route not found.",
                PaymentTransaction = paymentTransaction
            });
        }
    }
}