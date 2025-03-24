namespace Caas.Application.Features.Cats.Commands.CreateCats;

public sealed record CreateCatsApiResponse(
    [property: JsonPropertyName("id")]
    string Id,
    [property: JsonPropertyName("url")]
    string Url,
    [property: JsonPropertyName("width")]
    int Width,
    [property: JsonPropertyName("height")]
    int Height,
    [property: JsonPropertyName("breeds")]
    List<Breed> Breeds
);

public sealed record Breed(
    [property: JsonPropertyName("temperament")]
    string Temperament
);
