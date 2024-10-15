namespace SyncHms.Events.Handlers.Fias;

/// <summary>
/// Класс, описывающий обработчик события <see cref="FiasConnection"/>
/// оповещающего об изменении состояния сервиса <c>FIAS</c><br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class FiasConnectionHandler : Handler<FiasConnection>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="FiasConnection"/>.
    /// Отправляет в шину данных событие <see cref="FiasConnection"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override Task HandleAsync(FiasConnection @in, IEventContext context)
    {
        if (!@in.Connected)
            context.Break();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="FiasConnection"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string? Message(FiasConnection @in) => @in.Message;
}

