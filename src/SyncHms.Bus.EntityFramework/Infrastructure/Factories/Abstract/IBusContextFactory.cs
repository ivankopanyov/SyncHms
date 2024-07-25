namespace SyncHms.Bus.EntityFramework.Infrastructure.Factories.Abstract;

internal interface IBusContextFactory
{
    BusContext Create();
}