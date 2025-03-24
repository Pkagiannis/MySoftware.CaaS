namespace Caas.Application.Features.Cats.Queries.GetCatByCatId;

public class GetCatByCatIdQueryValidator : AbstractValidator<GetCatByCatIdQuery>
{
    public GetCatByCatIdQueryValidator()
    {
        RuleFor(x => x.CatId).NotEmpty().WithMessage("CatId is required");
    }
}