namespace SyncHms.Domain.EntityFramework.Infrastructure.Factories.Abstract;

internal interface IDomainContextFactory
{
    DomainContext Create();
}