namespace CaaS.Infrastructure.DbContexts;

public class CaasDbContext(
    DbContextOptions<CaasDbContext> options)
    : DbContext(options)
{
    public DbSet<Cat> Cats { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        ChangeTracker.SetAuditFields();
        return base.SaveChangesAsync(ct);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}