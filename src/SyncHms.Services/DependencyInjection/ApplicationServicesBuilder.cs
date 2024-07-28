namespace SyncHms.Services.DependencyInjection;

internal class ApplicationServicesBuilder(IServiceCollection services) : IApplicationServicesBuilder
{
    public IServiceCollection Services => services;
}