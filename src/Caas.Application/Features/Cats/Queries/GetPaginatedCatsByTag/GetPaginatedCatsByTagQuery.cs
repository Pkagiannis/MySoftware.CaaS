namespace Caas.Application.Features.Cats.Queries.GetPaginatedCatsByTag;

public sealed record GetPaginatedCatsByTagQuery(
    int Page,
    int PageSize,
    string Tag
) : IQuery<Result<GetPaginatedCatsResponse>>;

public sealed record GetPaginatedCatsResponse(
    PaginatedItems<CatDto> PaginatedCats
    );
