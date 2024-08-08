namespace SyncHms.Events.Services.Implement;

internal class CheckNumberService(ICache cache) : ICheckNumberService
{
    private const string CheckNumberKey = "CheckNumber";

    private const int CheckNumberMin = 1;

    private const int CheckNumberMax = 9999;
    
    private readonly SemaphoreSlim Semaphore = new(1);
    
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
