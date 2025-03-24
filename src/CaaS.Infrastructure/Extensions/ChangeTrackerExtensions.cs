namespace CaaS.Infrastructure.Extensions;
public static class ChangeTrackerExtensions
{
    /// <summary>
    /// Set audit fields for entities that implement the IAuditableEntity interface
    /// </summary>
    /// <param name="changeTracker"></param>
    public static void SetAuditFields(this ChangeTracker changeTracker)
    {
        IEnumerable<EntityEntry<AuditableEntity>> entries = changeTracker
            .Entries<AuditableEntity>()
            .Where(e => e.Entity is AuditableEntity &&
                        e.State == EntityState.Added);

        foreach (EntityEntry<AuditableEntity> entry in entries)
        {
            if (entry.Entity is AuditableEntity auditableEntity &&
                entry.State == EntityState.Added)
            {
                auditableEntity.Created = DateTime.UtcNow;
            }
        }
    }
}
