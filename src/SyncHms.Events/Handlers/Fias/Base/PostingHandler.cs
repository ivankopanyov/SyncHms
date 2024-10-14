namespace SyncHms.Events.Handlers.Fias.Base;

/// <summary>
/// Абстрактный класс, описывающий обработчик событий,
/// связанных с платежными начисдениями.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
/// <typeparam name="TIn">Тип обрабатываемого события.</typeparam>
/// <param name="fiasService">Экземпляр сервиса для взаимодействия с системой <c>FIAS</c></param>
internal abstract class PostingHandler<TIn>(IFiasService fiasService) : Handler<TIn>
{
    /// <summary>Минимально возможный номер чека.</summary>
    public const int CheckNumberMin = 1;

    /// <summary>Максимально возможный номер чека.</summary>
    public const int CheckNumberMax = 9999;

    private readonly object _lock = new();

    /// <summary>
    /// Метод, обновляющий значение переменной окружения,
    /// хранящей номер следующего чека.
    /// </summary>
    /// <param name="context">
    /// Экземпляр контекста обработки события, для отправки
    /// экземпляра обновленных знечений переменных окружения в шину данных.
    /// </param>
    /// <returns>Номер текущего чека.</returns>
    protected int GetCheckNumber(IEventContext context)
    {
        int checkNumber;

        lock (_lock)
        {
            checkNumber = fiasService.Environment.NextMicrosCheckNumber++;
            if (fiasService.Environment.NextMicrosCheckNumber > CheckNumberMax)
                fiasService.Environment.NextMicrosCheckNumber = CheckNumberMin;
        }

        context.Send(fiasService.Environment);
        return checkNumber;
    }
}
