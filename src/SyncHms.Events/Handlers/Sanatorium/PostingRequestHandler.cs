namespace SyncHms.Events.Handlers.Sanatorium;

internal class PostingRequestHandler(ISanatoriumService sanatoriumService) : Handler<PostTransactionsRequest>
{
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

            var number = 0;
            var checks = sanatoriumService.Environment.TaxCodes
                .Select(item => @in.Transactions
                    .SelectMany(t => t.Items)
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

    protected override string? Message(PostTransactionsRequest @in)
    {
        var result = $"Correlation ID: {@in.CorrelationId}";

        if (!string.IsNullOrWhiteSpace(@in.ReservationGuestId))
            result += $", Reservation: {@in.ReservationGuestId}";

        if (!string.IsNullOrWhiteSpace(@in.FolioGenericNo))
            result = $", FolioGenericNo: {@in.FolioGenericNo}";

        return result;
    }
}
