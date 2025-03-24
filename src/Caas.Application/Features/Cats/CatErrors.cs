namespace Caas.Application.Features.Cats;

public static class CatErrors
{
    public static Error NotFound(string catId) => new Error(
        "Cat.NotFound", $"Cat with Id '{catId}' was not found");

    public static readonly Error NoData = new Error(
        "Cat.NoData", "CaaS api returned no cats data");

    public static readonly Error NoNewCats = new Error(
        "Cat.NoNewCats", "No new cats to add");
}
