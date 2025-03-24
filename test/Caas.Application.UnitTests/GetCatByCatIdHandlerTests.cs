namespace Caas.Application.UnitTests.Features.Cats.Queries.GetCatByCatId;

public class GetCatByCatIdHandlerTests
{
    private readonly ICatRepository _mockCatRepository;
    private readonly GetCatByCatIdHandler _handler;

    public GetCatByCatIdHandlerTests()
    {
        _mockCatRepository = Substitute.For<ICatRepository>();
        _handler = new GetCatByCatIdHandler(_mockCatRepository);
    }

    [Fact]
    public async Task Handle_CatExists_ReturnsCatWithTags()
    {
        // Arrange
        var tag1 = Tag.Create("tag1");
        var tag2 = Tag.Create("tag2");
        var cat = Cat.Create("cat1", 100, 200, [], [tag1, tag2]);

        _mockCatRepository.GetCatAsync(cat.CatId, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Cat?>(cat));

        var query = new GetCatByCatIdQuery(cat.CatId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Match(
            onSuccess: response =>
            {
                Assert.Equal(cat.CatId, response.Cat.CatId);

                var responseTagNames = response.Cat.Tags.ToList();
                var expectedTagNames = cat.Tags.Select(ct => ct.Name).ToList();
                Assert.Equal(expectedTagNames.Count, responseTagNames.Count);
                foreach (var tagName in expectedTagNames)
                {
                    Assert.Contains(tagName, responseTagNames);
                }

                return true;
            },
            onFailure: error =>
            {
                Assert.Fail("Expected success but got failure");
                return false;
            });
    }

    [Fact]
    public async Task Handle_CatDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var catId = "cat123";
        _mockCatRepository.GetCatAsync(catId, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Cat?>(null));

        var query = new GetCatByCatIdQuery(catId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Match(
            onSuccess: response =>
            {
                Assert.Fail("Expected failure but got success");
                return false;
            },
            onFailure: error =>
            {
                Assert.Equal(CatErrors.NotFound(catId), error);
                return true;
            });
    }
}
