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

            var invoiceGenericNo = @in.Transactions.SelectMany(t => t.Items).Any(i => !string.IsNullOrWhiteSpace(i.ServiceItemCode))
                ? sanatoriumService.Environment.Rvc.ToString("000.##") + @in.InvoiceGenericNo
                : DateTime.Now.ToString("yyyyMMddhhmmssfffffff");

            context.Send(new PostRequestInfo()
            {
                CorrelationId = @in.CorrelationId,
                ReservationGuestId = @in.ReservationGuestId,
                InvoiceGenericNo = invoiceGenericNo,
                FolioGenericNo = @in.FolioGenericNo,
                Transactions = @in.Transactions?.Select(t => new TransactionInfo()
                {
                    ScheduleDate = t.ScheduleDate,
                    TransactionCode = t.TransactionCode,
                    Name = t.Name,
                    Items = t.Items?.Select(d => new TransactionDetailsInfo()
                    {
                        ItemKind = (int)d.ItemKind,
                        Name = d.Name,
                        Amount = d.Amount,
                        ServiceItemCode = d.ServiceItemCode,
                        Count = d.Count,
                        TransactionCode = d.TransactionCode
                    }).ToHashSet() ?? []
                }).ToHashSet() ?? []
            });
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
