namespace Caas.Application.Common.Pagination;

public record PaginationRequest(
    int PageSize,
    int PageIndex
    );
