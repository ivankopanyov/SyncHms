namespace SyncHms.Identity.Infrastructure;

internal class DatabaseInitializator : BackgroundService
{
    public DatabaseInitializator(IServiceScopeFactory serviceScopeFactory,
        IdentityOptions options)
    {
        var serviceScope = serviceScopeFactory.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<IdentityContext>();
        DatabaseHelper.InitializeDatabase(context, options.InitDatabase);
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}
