namespace Caas.Application.Features.Cats.Commands.CreateCats;

public class CreateCatsHandler(
    ICatRepository catRepository,
    ITagRepository tagRepository,
    IHttpClientFactory httpClientFactory)
    : ICommandHandler<CreateCatsCommand, Result<CreateCatsResponse>>
{
    public const string HttpClientName = nameof(CreateCatsHandler);
    private readonly ICatRepository _catRepository = catRepository;
    private readonly ITagRepository _tagRepository = tagRepository;
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(HttpClientName);

    public async Task<Result<CreateCatsResponse>> Handle(CreateCatsCommand command, CancellationToken ct)
    {
        var fetchedCatsData = await _httpClient.GetFromJsonAsync<IEnumerable<CreateCatsApiResponse>>("/v1/images/search?limit=25");
        if (fetchedCatsData == null || !fetchedCatsData.Any())
        {
            return CatErrors.NoData;
        }

        var uniqueCatsData = await FilterExistingCats(fetchedCatsData, ct);
        if (!uniqueCatsData.Any())
        {
            return CatErrors.NoNewCats;
        }

        var uniqueTagNames = GetUniqueTagNames(uniqueCatsData);
        var existingTags = await _tagRepository.GetTagsByNameAsync(uniqueTagNames, ct, trackChanges: true);
        var newTags = GenerateMissingTags(uniqueTagNames, existingTags);
        var combinedTags = existingTags.Concat(newTags).ToList();

        var catsToAdd = await BuildNewCats(uniqueCatsData, combinedTags, ct);
        await _catRepository.AddCatsAsync(catsToAdd, ct);

        return new CreateCatsResponse(catsToAdd.Select(c => c.CatId));
    }

    private async Task<IEnumerable<CreateCatsApiResponse>> FilterExistingCats(
    IEnumerable<CreateCatsApiResponse> fetchedCatsData,
    CancellationToken ct)
    {
        var apiCatIds = fetchedCatsData.Select(c => c.Id).ToList();
        var existingCatIds = await _catRepository.GetExistingCatIdsAsync(apiCatIds, ct);
        return fetchedCatsData.Where(c => !existingCatIds.Contains(c.Id)).ToList();
    }

    private static HashSet<string> GetUniqueTagNames(IEnumerable<CreateCatsApiResponse> cats)
    {
        return cats
            .Where(c => !string.IsNullOrEmpty(c.Breeds?.FirstOrDefault()?.Temperament))
            .SelectMany(c => c.Breeds.First().Temperament.Split(',', StringSplitOptions.TrimEntries))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
    }

    private static IEnumerable<Tag> GenerateMissingTags(HashSet<string> tagNames, IEnumerable<Tag> existingTags)
    {
        var existingTagNames = existingTags
            .Select(t => t.Name.ToLowerInvariant())
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        return tagNames
            .Select(t => t.ToLowerInvariant())
            .Where(tagName => !existingTagNames.Contains(tagName))
            .Distinct()
            .Select(tagName => Tag.Create(tagName))
            .ToList();
    }

    private async Task<Cat[]> BuildNewCats(IEnumerable<CreateCatsApiResponse> uniqueCatsData, IEnumerable<Tag> combinedTags, CancellationToken ct)
    {
        return await Task.WhenAll(uniqueCatsData.Select(async catData =>
        {
            var catTags = ExtractTagsFromCatData(catData, combinedTags);
            var image = await ConvertImageToByteArray(catData.Url);
            return Cat.Create(catData.Id, catData.Width, catData.Height, image, catTags.ToList());
        }).ToList());
    }

    private static IEnumerable<Tag> ExtractTagsFromCatData(CreateCatsApiResponse catData, IEnumerable<Tag> combinedTags)
    {
        string[] temperamentTags = catData.Breeds?.FirstOrDefault()?.Temperament
            ?.Split(',', StringSplitOptions.TrimEntries)
            ?? Array.Empty<string>();

        return combinedTags
            .Where(tag => temperamentTags.Contains(tag.Name, StringComparer.OrdinalIgnoreCase))
            .ToList();
    }

    private async Task<byte[]> ConvertImageToByteArray(string imageUrl) =>
        await _httpClient.GetByteArrayAsync(imageUrl);
}
