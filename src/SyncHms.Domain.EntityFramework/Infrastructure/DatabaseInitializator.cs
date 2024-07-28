namespace SyncHms.Domain.EntityFramework.Infrastructure;

internal class DatabaseInitializator : BackgroundService
{
    public DatabaseInitializator(IServiceScopeFactory serviceScopeFactory,
        EntityFrameworkDomainOptions options)
    {
        var serviceScope = serviceScopeFactory.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<DomainContext>();
        DatabaseHelper.InitializeDatabase(context, options.InitDatabase);
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}