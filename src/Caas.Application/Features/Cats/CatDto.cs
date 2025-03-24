namespace Caas.Application.Features.Cats;

public sealed record CatDto(
    string CatId,
    int Width,
    int Height,
    byte[] Image,
    IEnumerable<string> Tags
);