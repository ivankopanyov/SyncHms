namespace SyncHms.Server.Handlers;

/// <summary>
/// Класс, описывающий обработчик обновления значений переменных окружения.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
/// <param name="environmentRepository">Экземпляр репозитория для работы с окружением.</param>
/// <param name="hubContext">Экземпляр контекста концентратора окружения <c>SignalR</c></param>
public class UpdatedEnvironmentHandler(IEnvironmentRepository<ApplicationEnvironment> environmentRepository,
    IHubContext<EnvironmentHub> hubContext) : Handler<ApplicationEnvironment>
{
    /// <summary>
    /// Метод, обрабатывающий обновления значений переменных окружения.<br/>
    /// Сохраняет новые значения в репозиторий и отправляет обновленную
    /// информацию клиентам приложения с помощью <c>SignalR</c>
    /// </summary>
    /// <param name="in">Событие, содержащее обновленные значения переменных окружения.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(ApplicationEnvironment @in, IEventContext context)
    {
        if (await environmentRepository.UpdateAsync(@in))
            await hubContext.Clients.All.SendAsync("Environment", @in);
    }
}
