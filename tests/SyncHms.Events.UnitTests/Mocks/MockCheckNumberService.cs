namespace SyncHms.Events.UnitTests.Mocks;

public class MockCheckNumberService : ICheckNumberService
{
    public Task<int> GetCheckNumberAsync() => Task.FromResult(default(int));
}