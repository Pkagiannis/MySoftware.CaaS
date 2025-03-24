namespace Caas.Application.Features.Cats.Queries.GetCatByCatId;

public class GetCatByCatIdHandler(
        ICatRepository catRepository)
    : IQueryHandler<GetCatByCatIdQuery, Result<GetCatByCatIdResponse>>
{
    private readonly ICatRepository _catRepository = catRepository;

    public async Task<Result<GetCatByCatIdResponse>> Handle(GetCatByCatIdQuery query, CancellationToken ct)
    {
        var cat = await _catRepository.GetCatAsync(query.CatId, ct);
        if (cat is null)
        {
            return CatErrors.NotFound(query.CatId);
        }

        return new GetCatByCatIdResponse(cat.ToCatDto());
    }
}
