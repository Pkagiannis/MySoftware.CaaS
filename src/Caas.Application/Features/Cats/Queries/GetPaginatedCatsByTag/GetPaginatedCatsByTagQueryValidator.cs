namespace Caas.Application.Features.Cats.Queries.GetPaginatedCatsByTag;

public class GetPaginatedCatsByTagQueryValidator : AbstractValidator<GetPaginatedCatsByTagQuery>
{
    public GetPaginatedCatsByTagQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page must be greater than 0");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than 0");
    }
}