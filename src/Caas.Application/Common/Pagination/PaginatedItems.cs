namespace Caas.Application.Common.Pagination;

public record PaginatedItems<TEntity>(
    int PageIndex,
    int PageSize,
    long Count,
    IEnumerable<TEntity> Data
    ) where TEntity : class;
