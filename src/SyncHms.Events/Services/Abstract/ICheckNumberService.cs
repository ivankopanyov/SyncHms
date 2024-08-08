namespace SyncHms.Events.Services.Abstract;

internal interface ICheckNumberService
{
    Task<int> GetCheckNumberAsync();
}
