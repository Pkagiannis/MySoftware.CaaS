namespace Caas.Application.Features.Cats.Commands.CreateCats;

public sealed record CreateCatsCommand()
    : ICommand<Result<CreateCatsResponse>>;

public sealed record CreateCatsResponse(
    IEnumerable<string> CreatedCatIds
    );
