namespace CaaS.Domain.Abstractions;

public interface ITagRepository
{
    Task<List<Tag>> GetTagsByNameAsync(IEnumerable<string> tagNames, CancellationToken ct, bool trackChanges = false);
}