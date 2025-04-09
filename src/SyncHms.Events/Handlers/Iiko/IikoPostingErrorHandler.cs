namespace SyncHms.Events.Handlers.Iiko;

public class IikoPostingErrorHandler : Handler<List<IikoPostingError>>
{
    protected override Task HandleAsync(List<IikoPostingError> @in, IEventContext context)
    {
        if (@in.Count == 0)
        {
            context.Logiable = false;
            return Task.CompletedTask;
        }

        var error = @in.First();
        context.SetMessage(error.ToString());
        @in.Remove(error);

        if (@in.Count == 0)
        {
            context.Break(error.Message);
            return Task.CompletedTask;
        }
        
        context.MarkWithError(error.Message);
        context.Send(@in);
        return Task.CompletedTask;
    }
}