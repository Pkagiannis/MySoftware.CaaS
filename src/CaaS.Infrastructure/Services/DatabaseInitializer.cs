namespace CaaS.Infrastructure.Services;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly CaasDbContext _dbContext;
    private readonly string _dbContextName = nameof(CaasDbContext);
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(CaasDbContext dbContext, ILogger<DatabaseInitializer> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task MigrateDbAsync()
    {
        try
        {
            _logger.LogInformation($"Migrating {_dbContextName}...");
            await _dbContext.Database.MigrateAsync();
            _logger.LogInformation($"Migration of {_dbContextName} was successful.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating {DbContextName}: {Message}", _dbContextName, ex.InnerException?.Message ?? ex.Message);
            throw;
        }
    }
}
