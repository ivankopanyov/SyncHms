namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик события <see cref="UpdatedProfileFailed"/>,
/// оповещающего об ошибке во время обработки обновленного профиля.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class UpdatedProfileFailedHandler : Handler<UpdatedProfileFailed>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="UpdatedProfileFailed"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override Task HandleAsync(UpdatedProfileFailed @in, IEventContext context)
    {
        context.Break(@in.ErrorMessage);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="UpdatedProfileFailed"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(UpdatedProfileFailed @in) => $"Profile: {@in.ProfileNumber:0}";
}
