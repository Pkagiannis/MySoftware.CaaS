namespace CaaS.Infrastructure.Repositories;

public class CatRepository(
    CaasDbContext dbContext)
    : ICatRepository
{
    private readonly CaasDbContext _dbContext = dbContext;

    public async Task<List<string>> GetExistingCatIdsAsync(
        List<string> apiCatIds,
        CancellationToken ct,
        bool trackChanges = false)
    {
        return await _dbContext.Cats
            .ApplyTracking(trackChanges)
            .Where(c => apiCatIds.Contains(c.CatId))
            .Select(c => c.CatId)
            .ToListAsync();
    }

    public async Task<Cat?> GetCatAsync(
        string apiCatId,
        CancellationToken ct,
        bool trackChanges = false)
    {
        return await _dbContext.Cats
            .ApplyTracking(trackChanges)
            .Include(c => c.Tags)
            .Where(c => c.CatId == apiCatId)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<(List<Cat> Cats, int TotalCount)> GetPaginatedCatsByTagAsync(
        int page,
        int pageSize,
        string? tag,
        CancellationToken ct,
        bool trackChanges = false)
    {
        IQueryable<Cat> query = _dbContext.Cats
            .ApplyTracking(trackChanges)
            .Include(c => c.Tags)
            .AsQueryable();

        if (!string.IsNullOrEmpty(tag))
        {
            query = query.Where(c => c.Tags.Any(ct => ct.Name == tag));
        }

        int totalCount = await query.CountAsync();

        if (totalCount == 0)
        {
            return (new List<Cat>(), totalCount);
        }

        List<Cat> cats = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (cats, totalCount);
    }

    public async Task AddCatsAsync(
        IEnumerable<Cat> cats,
        CancellationToken ct)
    {
        await _dbContext.Cats.AddRangeAsync(cats);
        await _dbContext.SaveChangesAsync(ct);
    }
}
