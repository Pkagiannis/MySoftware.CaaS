namespace CaaS.Domain.Entities.Common;

public abstract class Entity
{
    [Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}
