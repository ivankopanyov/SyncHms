namespace SyncHms.Events.Handlers.Iiko;

/// <summary>
/// Класс, описывающий обработчик задачи на получений оплат из системы <c>IIKO</c>,
/// инициированных планировщиком событий.<br/>
/// Унаследован от класса <see cref="ScheduleHandler"/>
/// </summary>
/// <param name="iikoService">Экземпляр сервиса взаимодействия с системой <c>IIKO</c></param>
[ScheduleDescription("Мониторинг оплат и возвратов в системе IIKO.")]
public class PaymentTransactionsScheduleHandler(IIikoService iikoService) : ScheduleHandler
{
    /// <summary>
    /// Метод, запрашивающий оплаты из системы <c>IIKO</c>.<br/>
    /// Переопределяет метод <see cref="ScheduleHandler.HandleAsync"/>
    /// </summary>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(IScheduleEventContext context)
    {
        try
        {
            var token = await iikoService.LoginAsync();

            try
            {
                var today = DateOnly.FromDateTime(DateTime.Now);
                var yesterday = today.AddDays(-1);
                var tomorrow = today.AddDays(1);
                var previous = context.Previous.AddSeconds(-iikoService.TimeShiftSeconds);
                var current = context.Current.AddSeconds(-iikoService.TimeShiftSeconds);
                
                var paymentTransactions = await iikoService.GetPaymentTransactionsAsync(token, yesterday, tomorrow,
                    new DateTimeRangeFilter("CloseTime", previous, current));

                var orderReturned = await iikoService.GetOrderReturnedAsync(token, previous, current);
                
                IEnumerable<List<PaymentTransaction>> orders = [];
                if (orderReturned.Count > 0)
                {
                    var returnOrders = await iikoService.GetPaymentTransactionsAsync(token, yesterday, tomorrow,
                        new ValuesFilter<long>("OrderNum", orderReturned.Select(o => o.CheckNumber).ToArray()));

                    orders = returnOrders
                        .Where(p => p.DishSumInt < 0)
                        .GroupBy(p => p.OrderNumber)
                        .Select(g => g.ToList());

                    var newPayments = await iikoService.GetPaymentTransactionsAsync(token, yesterday, tomorrow,
                        new ValuesFilter<long>("OrderNum", orderReturned
                            .Select(o => o.NewCheckNumber)
                            .OfType<long>()
                            .Where(cn => paymentTransactions.All(pt => pt.OrderNumber != cn))
                            .ToArray()));

                    paymentTransactions.AddRange(newPayments);
                }

                var payments = paymentTransactions
                    .Where(p => p.DishSumInt > 0)
                    .GroupBy(p => p.OrderNumber)
                    .Select(g => g.ToList());

                List<List<PaymentTransaction>> transactions = [];
                transactions.AddRange(payments);
                transactions.AddRange(orders);
                transactions.ForEach(t => t.ForEach(p =>
                {
                    var divider = -p.DishReturnSum / (p.DishSumInt - p.DiscountSum + p.IncreaseSum + p.DishReturnSum) + 1;
                    p.DishSumInt /= divider;
                    p.DiscountSum /= divider;
                    p.IncreaseSum /= divider;
                    p.DishReturnSum = 0;
                }));
                
                foreach (var transaction in transactions)
                {
                    var transactionItem = transaction.First();
                    var iikoPaymentTransaction = new IikoPaymentTransaction
                    {
                        CheckNumber = transactionItem.OrderNumber.ToString(),
                        CloseDateTime = transactionItem.DishSumInt < 0 &&
                            orderReturned.FirstOrDefault(i => i.CheckNumber == transactionItem.OrderNumber) is { } orderReturn
                            ? orderReturn.DateTime
                            : transactionItem.CloseDateTime,
                        Items = transaction.ToList()
                    };

                    var postings = iikoPaymentTransaction.GetPostings(iikoService.PaymentTypes, iikoService.SalesOutlet,
                        iikoService.Categories, iikoService.DiscountCategoryName, iikoService.IncreaseCategoryName);
                    
                    if (postings.Queue.Count > 0)
                    {
                        postings.Current = postings.Queue.First();
                        postings.Queue.Remove(postings.Current);
                        postings.Check = iikoPaymentTransaction.ToCheck(iikoService.Environment.CheckLocalizationCode,
                            iikoService.Environment.CheckHeader);
                        
                        context.Send(postings);
                    }
                    else if (postings.PostingErrors.Count > 0)
                    {
                        context.Send(postings.PostingErrors);
                    }
                }
            }
            finally
            {
                try
                {
                    await iikoService.LogoutAsync(token);
                }
                catch (HttpRequestException ex)
                {
                    var message = ex.StatusCode != null ? $"{ex.StatusCode}: {ex.Message}" : ex.Message;
                    context.MarkWithError(message, ex.StackTrace);
                }
                catch (Exception ex)
                {
                    context.MarkWithError(ex.Message, ex.StackTrace);
                }
            }
        }
        catch (HttpRequestException ex)
        {
            context.Send(new PaymentTransactionsError(ex, ex.StatusCode));
            throw;
        }
        catch (Exception ex)
        {
            context.Send(new PaymentTransactionsError(ex));
            throw;
        }
    }
}