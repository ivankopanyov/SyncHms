namespace SyncHms.Events.Handlers.Fiscal;

/// <summary>
/// Класс, описывающий обработчик события <see cref="MicrosPostingRequest"/>,
/// оповещающего о совершении денежного платежа или начислении платежа на номер.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class MicrosPostingRequestHandler(IFiscalService fiscalService) : Handler<MicrosPostingRequest>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="MicrosPostingRequest"/>.
    /// Проводит попытку сохранения чека платежа в базе данных <c>MICROS</c>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(MicrosPostingRequest @in, IEventContext context)
    {
        try
        {
            var cultureInfo = CultureInfo.InvariantCulture;
            var rvc = fiscalService.Environment.Rvc;
            var total = @in.Transactions.Select(t => t.Amount).Sum();
            var stringTotal = total.ToString("0.00", cultureInfo);

            var fiscalCheck = new FiscalCheck
            {
                uws = 1,
                rvc = rvc,
                open_time = @in.DateTime,
                close_time = @in.DateTime,
                total = stringTotal
            };

            var checkItems = @in.Transactions.SelectMany(t => TransactionDetailsToCheckItem(t, cultureInfo)).ToList();

            checkItems.Add(new CheckItem
            {
                type = 'T',
                name = @in.PaymentMethod ?? string.Empty,
                ttl = stringTotal
            });

            fiscalCheck.dtl = checkItems.ToArray();
            var taxItemsTotal = @in.Transactions.Select(t => fiscalService.Environment.TaxCodes.TryGetValue(t.ServiceItemCode, out var tax) && tax ? t.Amount : 0).Sum();

            if (taxItemsTotal != 0)
            {
                fiscalCheck.tax =
                [
                    new CheckTax
                    {
                        type = 'I',
                        taxnum = 1,
                        name = "НДС",
                        rate = 20,
                        ttl = (total / 6).ToString("0.00", cultureInfo),
                        txbl = stringTotal
                    }
                ];
            }

            var fiscalCheckNumber = await fiscalService.SetCheckAsync(fiscalCheck);
            var rvcString = rvc.ToString().PadLeft(3, '0');
            var checkNumber = fiscalCheckNumber.ToString().PadLeft(4, '0');
            var microsCheckNumber = $"{rvcString}{checkNumber}";

            context.SetMessage($"{@in}, MicrosCheckNumber: {microsCheckNumber}");
            Send(@in, context);
        }
        catch (Exception ex)
        {
            if (!fiscalService.Environment.SyncPostingOpera)
            {
                context.Send(new PostTransactionsResponse(@in.CorrelationId)
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message
                });
            }
            else
            {
                context.MarkWithError(ex.Message, ex.StackTrace);
                Send(@in, context);
            }
        }
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="MicrosPostingRequest"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(MicrosPostingRequest @in) => @in.ToString();

    /// <summary>Метод отправления события в шину данных.</summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    private static void Send(MicrosPostingRequest @in, IEventContext context)
    {
        context.Send(new PostTransactionsResponse(@in.CorrelationId)
        {
            Succeeded = true,
            ErrorMessage = @in.ToString()
        });
    }

    /// <summary>Метод формиравания элементов чека.</summary>
    private CheckItem[] TransactionDetailsToCheckItem(TransactionDetails transactionDetails, CultureInfo cultureInfo)
    {
        var qty = (int)transactionDetails.Count;
        var qtf = (double)transactionDetails.Count;
        var name = transactionDetails.Name ?? string.Empty;

        const int limitLength = 15;
        List<string> items = [];

        for (var i = 0; i < name.Length; i += limitLength)
        {
            var to = Math.Min(i + limitLength, name.Length);
            items.Add(name[i..to]);
        }

        List<CheckItem> checkItems =
        [
            new()
            {
                type = 'M',
                name = items.Count == 0 ? string.Empty : items[0],
                qty = qty,
                qtf = qtf,
                ttl = transactionDetails.AmountBeforeDiscount.ToString("0.00", cultureInfo),
                tax = (byte)(fiscalService.Environment.TaxCodes.TryGetValue(transactionDetails.ServiceItemCode,
                    out var tax) && tax ? 128 : 0),
                mgnum = 1,
                mgname = name,
                fgnum = 1,
                fgname = name
            }
        ];

        for (var i = 1; i < items.Count; i++)
        {
            checkItems.Add(new CheckItem
            {
                type = 'R',
                name = items[i]
            });
        }

        if (transactionDetails.AmountBeforeDiscount != 0 && transactionDetails.Amount != transactionDetails.AmountBeforeDiscount)
        {
            var discount = transactionDetails.Amount - transactionDetails.AmountBeforeDiscount;
            checkItems[0].dsc = discount.ToString("0.00", cultureInfo);
            checkItems.Add(new CheckItem
            {
                type = 'D',
                objnum = 13,
                name = $"Скидка {(int)((1 - discount / transactionDetails.AmountBeforeDiscount) * -100)}%",
                qty = qty,
                qtf = qtf
            });
        }

        return checkItems.ToArray();
    }
}
