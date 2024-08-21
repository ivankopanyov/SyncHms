namespace SyncHms.Events.Services.Implement;

/// <summary>
/// Класс, описывающий сервис, предоставляющий номера чеков для сохранения в базу данных <c>MICROS</c><br/>
/// Реализует интерфейс <see cref="ICheckNumberService"/>
/// </summary>
/// <param name="cache">Экземпляр сервиса кеширования.</param>
internal class CheckNumberService(ICache cache) : ICheckNumberService
{
    /// <summary>Ключ объекта, хранящего текущий номер чека в базе данных.</summary>
    private const string CheckNumberKey = "CheckNumber";

    /// <summary>Минимально возможный номер чека.</summary>
    private const int CheckNumberMin = 1;

    /// <summary>максимально возможный номер чека.</summary>
    private const int CheckNumberMax = 9999;
    
    private readonly SemaphoreSlim Semaphore = new(1);
    
    /// <summary>Метод, возвращающий следующий номер чека.</summary>
    /// <returns>Номер чека.</returns>
    public async Task<int> GetCheckNumberAsync()
    {
        await Semaphore.WaitAsync();
        
        try
        {
            if (await cache.PopAsync<CheckNumber>(CheckNumberKey) is not { } checkNumber)
            {
                checkNumber = new CheckNumber
                {
                    Value = 1
                };
            }

            var checkNumberValue = Math.Min(Math.Max(CheckNumberMin, checkNumber.Value), CheckNumberMax);
            checkNumber.Value = checkNumberValue == CheckNumberMax ? CheckNumberMin : checkNumberValue + 1;
            await cache.PushAsync(CheckNumberKey, checkNumber);
            return checkNumberValue;
        }
        finally
        {
            Semaphore.Release();
        }
    }
}
