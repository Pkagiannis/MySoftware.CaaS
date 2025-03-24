namespace Caas.Application.Features.Cats.Queries.GetCatByCatId;

public sealed record GetCatByCatIdQuery(string CatId)
    : IQuery<Result<GetCatByCatIdResponse>>;

public sealed record GetCatByCatIdResponse(
    CatDto Cat
    );
