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
    /// В случае, если параметры окружения <see cref="ApplicationEnvironment.SyncPostingOpera"/>
    /// и <see cref="ApplicationEnvironment.SyncPostingMicros"/> равны <c>false</c>,
    /// отправляет в шину данных событие <see cref="PostTransactionsResponse"/>.<br/>
    /// В случае, если параметр окружения <see cref="ApplicationEnvironment.SyncPostingOpera"/>
    /// равен <c>false</c>, а параметр <see cref="ApplicationEnvironment.SyncPostingMicros"/>
    /// равен <c>true</c>, отправляет в шину данных событие <see cref="MicrosPostingRequest"/>.<br/>
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
            if (sanatoriumService.Environment is { SyncPostingOpera: false, SyncPostingMicros: false })
            {
                context.Send(new PostTransactionsResponse(@in.CorrelationId)
                {
                    Succeeded = true,
                    ErrorMessage = "SyncingOpera: disabled, SyncMicros: disabled"
                });

                return Task.CompletedTask;
            }

            var taxCodes = sanatoriumService.Environment.TaxCodes;
            PostingBase message;

            if (!sanatoriumService.Environment.SyncPostingOpera)
            {
                var microsPostingRequest = @in.ToMicrosPostingRequest(taxCodes);
                message = microsPostingRequest;
                context.Send(microsPostingRequest);
            }
            else
            {
                if (@in.IsRoomPosting())
                {
                    var roomNumberRequest = @in.ToRoomNumberRequest(taxCodes);
                    message = roomNumberRequest;
                    context.Send(roomNumberRequest);
                }
                else
                {
                    var fiasPostSimple = @in.ToFiasPostSimple(taxCodes);
                    message = fiasPostSimple;
                    context.Send(fiasPostSimple);
                }
            }
            
            context.SetMessage(message.ToString());
        }
        catch (Exception ex)
        {
            context.Send(new PostTransactionsResponse(@in.CorrelationId)
            {
                Succeeded = false,
                ErrorMessage = ex.Message
            });
        }

        return Task.CompletedTask;
    }
}
