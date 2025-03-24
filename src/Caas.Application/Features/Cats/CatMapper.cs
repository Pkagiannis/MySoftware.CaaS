namespace Caas.Application.Features.Cats;

public static class CatMapper
{
    public static CatDto ToCatDto(this Cat cat) =>
        new CatDto(
            cat.CatId,
            cat.Width,
            cat.Height,
            cat.Image,
            GetTagNames(cat));

    private static IEnumerable<string> GetTagNames(Cat cat) =>
        cat.Tags.Select(t => t.Name);
}
