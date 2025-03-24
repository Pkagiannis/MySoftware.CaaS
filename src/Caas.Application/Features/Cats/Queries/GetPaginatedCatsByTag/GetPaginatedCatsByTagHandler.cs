namespace Caas.Application.Features.Cats.Queries.GetPaginatedCatsByTag;

public class GetPaginatedCatsByTagHandler(
    ICatRepository catRepository
) : IQueryHandler<GetPaginatedCatsByTagQuery, Result<GetPaginatedCatsResponse>>
{
    private readonly ICatRepository _catRepository = catRepository;

    public async Task<Result<GetPaginatedCatsResponse>> Handle(GetPaginatedCatsByTagQuery query, CancellationToken ct)
    {
        var (cats, totalCount) = await _catRepository.GetPaginatedCatsByTagAsync(query.Page, query.PageSize, query.Tag, ct);

        var data = cats.Select(cat => cat.ToCatDto());

        var paginatedCats = new PaginatedItems<CatDto>(query.Page, query.PageSize, totalCount, data);

        return Result<GetPaginatedCatsResponse>.Success(new GetPaginatedCatsResponse(paginatedCats));
    }
}
