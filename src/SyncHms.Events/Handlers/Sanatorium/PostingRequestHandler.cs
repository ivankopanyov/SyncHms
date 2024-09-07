namespace SyncHms.Events.Handlers.Sanatorium;

/// <summary>
/// Класс, описывающий обработчик события <see cref="PostTransactionsRequest"/>,
/// оповещающего о совершении денежного платежа или начислении платежа на номер.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class PostingRequestHandler(ISanatoriumService sanatoriumService) : Handler<PostTransactionsRequest>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="PostTransactionsRequest"/>.<br/>
    /// В случае, если параметр окружения <see cref="ApplicationEnvironment.SyncPostingOpera"/>
    /// равен <c>false</c>, отправляет в шину данных событие <see cref="PostTransactionsResponse"/>.<br/>
    /// В случае начисления платежа на номер, отправляет в шину данных событие <see cref="RoomNumberRequest"/>.<br/>
    /// В случае совершения денежного платежа, отправляет в шину данных событие <see cref="FiasPostSimple"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override Task HandleAsync(PostTransactionsRequest @in, IEventContext context)
    {
        try
        {
            if (!sanatoriumService.Environment.SyncPostingOpera)
            {
                context.Send(new PostTransactionsResponse(@in.CorrelationId)
                {
                    Succeeded = true
                });

                return Task.CompletedTask;
            }

            var items = @in.Transactions.SelectMany(i => i.Items);
            var codes = sanatoriumService.Environment.TaxCodes.Keys.ToArray();
            
            if (items.FirstOrDefault(i => !string.IsNullOrEmpty(i.ServiceItemCode) && !codes.Contains(i.ServiceItemCode)) is { } item)
            {
                context.Send(new PostTransactionsResponse(@in.CorrelationId)
                {
                    Succeeded = false,
                    ErrorMessage = $"{item.Name} uses unsupported External ID {item.ServiceItemCode}."
                });

                return Task.CompletedTask;
            }

            var number = 0;
            var checks = sanatoriumService.Environment.TaxCodes
                .Select(item => items
                    .Where(i => i.ServiceItemCode == item.Key)
                    .Select(i => new FiscalCheckItem
                    {
                        ObjectNumber = int.TryParse(i.TransactionCode, out var objectNumber) &&
                                       objectNumber > 0 ? objectNumber : ++number,
                        Name = i.Name,
                        Quantity = (double)i.Count,
                        Total = i.Amount,
                        Tax = (byte)(item.Value ? 128 : 0),
                        TaxPosting = (int)i.ItemKind
                    }));
            
            
            if (!checks.Any(c => c.Any()))
            {
                context.Send(new PostTransactionsResponse(@in.CorrelationId)
                {
                    Succeeded = false,
                    ErrorMessage = "External ID is required."
                });

                return Task.CompletedTask;
            }
            
            var payments = @in.Transactions
                .Where(t => t.Items.Length > 0 && t.Items.Any(i => string.IsNullOrEmpty(i.ServiceItemCode)))
                .GroupBy(t => t.TransactionCode);

            if (payments.LastOrDefault() is { } payment)
            {
                context.Send(new FiasPostSimple
                {
                    CorrelationId = @in.CorrelationId,
                    Total = payment
                        .SelectMany(t => t.Items.Where(i => string.IsNullOrEmpty(i.ServiceItemCode)))
                        .Select(i => -i.Amount * 100).Sum(),
                    PmsPaymentMethod = payment.Key,
                    CheckNumber = @in.InvoiceGenericNo ?? string.Empty,
                    Checks = checks
                });
            }
            else
            {
                context.Send(new RoomNumberRequest
                {
                    CorrelationId = @in.CorrelationId,
                    ReservationGuestId = @in.ReservationGuestId,
                    FolioGenericNo = @in.FolioGenericNo,
                    CheckNumber = @in.Transactions?.FirstOrDefault()?.Name ?? string.Empty,
                    Checks = checks
                });
            }
        }
        catch (Exception ex)
        {
            context.Break(ex.Message, ex);
        }

        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="PostTransactionsRequest"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string? Message(PostTransactionsRequest @in) => $"Reservation: {@in.ReservationGuestId}";
}
