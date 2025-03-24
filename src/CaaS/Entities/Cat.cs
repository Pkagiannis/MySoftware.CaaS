namespace CaaS.Domain.Entities;

public class Cat : AuditableEntity
{
    private Cat()
    {
    }

    public string CatId { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public byte[] Image { get; set; }
    public List<Tag> Tags { get; set; } = [];

    public static Cat Create(string catId, int width, int height, byte[] image, List<Tag> tags) =>
    new Cat
    {
        CatId = catId,
        Width = width,
        Height = height,
        Image = image,
        Tags = tags
    };
}

public class TestConfig : AuditableEntityConfig<Cat>
{
    protected override void ConfigureProperties(EntityTypeBuilder<Cat> entity)
    {
        entity.Property(e => e.CatId).IsRequired().HasMaxLength(250);
        entity.Property(e => e.Width).IsRequired();
        entity.Property(e => e.Height).IsRequired();
        entity.Property(e => e.Image).IsRequired().HasColumnType("varbinary(max)");

        entity.HasMany(c => c.Tags)
              .WithMany(t => t.Cats)
              .UsingEntity<Dictionary<string, object>>(
                "CatTag",
                j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                j => j.HasOne<Cat>().WithMany().HasForeignKey("CatId"),
                j =>
                {
                    j.HasKey("CatId", "TagId");
                }
                );
    }
}