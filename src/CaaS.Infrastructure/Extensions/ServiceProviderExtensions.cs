namespace CaaS.Infrastructure.Extensions;

public static class ServiceProviderExtensions
{
    public static async Task MigrateDatabaseAsync(
        this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
        await databaseInitializer.MigrateDbAsync();
    }
}