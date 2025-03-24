namespace CaaS.Infrastructure.Repositories;

public class TagRepository(
    CaasDbContext context)
    : ITagRepository
{
    private readonly CaasDbContext _dbContext = context;

    public async Task<List<Tag>> GetTagsByNameAsync(
        IEnumerable<string> tagNames,
        CancellationToken ct,
        bool trackChanges = false)
    {
        return await _dbContext.Tags
            .ApplyTracking(trackChanges)
            .Where(t => tagNames.Contains(t.Name))
            .ToListAsync();
    }
}