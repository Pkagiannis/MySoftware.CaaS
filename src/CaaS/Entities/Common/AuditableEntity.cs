namespace CaaS.Domain.Entities.Common;

public abstract class AuditableEntity : Entity
{
    public DateTime Created { get; set; }
}

public abstract class AuditableEntityConfig<T>
    : IEntityTypeConfiguration<T>
    where T : AuditableEntity
{
    public void Configure(EntityTypeBuilder<T> entity)
    {
        entity.HasKey(e => e.Id);
        ConfigureProperties(entity);
    }

    protected abstract void ConfigureProperties(EntityTypeBuilder<T> entity);
}
