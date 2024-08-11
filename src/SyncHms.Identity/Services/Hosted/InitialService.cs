namespace SyncHms.Identity.Services.Hosted;

internal class InitialService : BackgroundService
{
    public InitialService(IIdentityContextFactory identityContextFactory, IdentityOptions options)
    {
        using var context = identityContextFactory.Create();
        if (options.UseMigrations)
            context.Database.Migrate();
        else
            context.Database.EnsureCreated();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;
}
