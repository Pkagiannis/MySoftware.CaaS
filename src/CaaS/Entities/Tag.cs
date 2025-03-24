namespace CaaS.Domain.Entities;

public class Tag : AuditableEntity
{
    private Tag()
    {
    }

    public string Name { get; set; }
    public List<Cat> Cats { get; set; } = [];


    public static Tag Create(string name) =>
        new Tag
        {
            Name = name
        };
}

public class TagConfig : AuditableEntityConfig<Tag>
{
    protected override void ConfigureProperties(EntityTypeBuilder<Tag> entity)
    {
        entity.Property(e => e.Name).IsRequired().HasMaxLength(250);
        entity.HasIndex(e => e.Name).IsUnique();
    }
}