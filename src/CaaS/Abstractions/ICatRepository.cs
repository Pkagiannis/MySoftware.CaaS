namespace CaaS.Domain.Abstractions;

public interface ICatRepository
{
    Task<List<string>> GetExistingCatIdsAsync(List<string> apiCatIds, CancellationToken ct, bool trackChanges = false);

    Task<Cat?> GetCatAsync(string apiCatId, CancellationToken ct, bool trackChanges = false);

    Task<(List<Cat> Cats, int TotalCount)> GetPaginatedCatsByTagAsync(int page, int pageSize, string tag, CancellationToken ct, bool trackChanges = false);

    Task AddCatsAsync(IEnumerable<Cat> cats, CancellationToken ct);
}
