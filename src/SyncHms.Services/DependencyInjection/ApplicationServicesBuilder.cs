namespace SyncHms.Services.DependencyInjection;

internal class ApplicationServicesBuilder(IServiceCollection services) :
    ServicesBusBuilder<ApplicationEnvironment>(services), IApplicationServicesBuilder
{
}
