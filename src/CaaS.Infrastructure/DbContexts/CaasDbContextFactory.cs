namespace CaaS.Infrastructure.DbContexts;

/// <summary>
/// Used to create the CaasDbContext for design-time tools like EF Core CLI
/// Use with dotnet ef migrations add <MigrationName>
/// </summary>
public class CaasDbContextFactory : IDesignTimeDbContextFactory<CaasDbContext>
{
    public CaasDbContext CreateDbContext(string[] args)
    {
        string connectionString = "Server=localhost,1433;Database=CaaSDb;User Id=sa;Password=Pwd12345^;TrustServerCertificate=True;";
        var optionsBuilder = new DbContextOptionsBuilder<CaasDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new CaasDbContext(optionsBuilder.Options);
    }
}
