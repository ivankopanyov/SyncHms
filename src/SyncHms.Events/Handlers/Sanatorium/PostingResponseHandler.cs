namespace SyncHms.Events.Handlers.Sanatorium;

internal class PostingResponseHandler(ISanatoriumService sanatoriumService) : Handler<PostTransactionsResponse>
{
    protected override async Task HandleAsync(PostTransactionsResponse @in, IEventContext context)
    {
        try
        {
            await sanatoriumService.SendPostTransactionsResponseAsync(@in);

            if (!@in.Succeeded)
                context.Break(@in.ErrorMessage);
        }
        catch (Exception ex)
        {
            context.Break(ex.Message, ex);
        }
    }

    protected override string? Message(PostTransactionsResponse @in)
    {
        var result = $"Correlation ID {@in.CorrelationId} {(@in.Succeeded ? "Succeeded" : "Error")}";
        
        if (!string.IsNullOrWhiteSpace(@in.ErrorMessage))
            result += $": {@in.ErrorMessage}";

        return result;
    }
}
