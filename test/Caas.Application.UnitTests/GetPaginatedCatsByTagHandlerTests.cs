namespace Caas.Application.UnitTests.Features.Cats.Queries.GetPaginatedCatsByTag;

public class GetPaginatedCatsByTagHandlerTests
{
    private readonly ICatRepository _mockCatRepository;
    private readonly GetPaginatedCatsByTagHandler _handler;

    public GetPaginatedCatsByTagHandlerTests()
    {
        _mockCatRepository = Substitute.For<ICatRepository>();
        _handler = new GetPaginatedCatsByTagHandler(_mockCatRepository);
    }

    [Fact]
    public async Task Handle_TagIsNull_ReturnsPaginatedCats()
    {
        // Arrange
        string tag = null!;
        var page = 1;
        var pageSize = 5;

        var cats = new List<Cat>();
        for (int i = 1; i <= 6; i++)
        {
            cats.Add(Cat.Create($"cat{i}", 100 + i, 200 + i, new byte[] { }, []));
        }

        _mockCatRepository.GetPaginatedCatsByTagAsync(page, pageSize, tag, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult((cats.Take(pageSize).ToList(), cats.Count)));

        var query = new GetPaginatedCatsByTagQuery(page, pageSize, tag);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Match(
            onSuccess: response =>
            {
                Assert.Equal(cats.Count, response.PaginatedCats.Count);
                Assert.Equal(pageSize, response.PaginatedCats.Data.Count());
                foreach (var catDto in response.PaginatedCats.Data)
                {
                    var cat = cats.FirstOrDefault(c => c.CatId == catDto.CatId);
                    Assert.NotNull(cat);
                    Assert.Equal(cat.CatId, catDto.CatId);
                    Assert.Equal(cat.Width, catDto.Width);
                    Assert.Equal(cat.Height, catDto.Height);
                    Assert.Equal(cat.Image, catDto.Image);
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
    public async Task Handle_TagHasValue_ReturnsPaginatedCatsWithTag()
    {
        // Arrange
        var tag1 = Tag.Create("tag1");
        var tag2 = Tag.Create("tag2");
        var page = 1;
        var pageSize = 5;
        int expectedCatCount = 3;

        var catsWithTag1 = CreateCatsWithTag(tag1, 3);
        var catsWithTag2 = CreateCatsWithTag(tag2, 3);
        var allCats = catsWithTag1.Concat(catsWithTag2).ToList();

        _mockCatRepository.GetPaginatedCatsByTagAsync(page, pageSize, tag1.Name, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult((catsWithTag1.Take(pageSize).ToList(), expectedCatCount)));

        var query = new GetPaginatedCatsByTagQuery(page, pageSize, tag1.Name);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Match(
            onSuccess: response =>
            {
                Assert.Equal(expectedCatCount, response.PaginatedCats.Count);
                Assert.Equal(expectedCatCount, response.PaginatedCats.Data.Count());
                foreach (var catDto in response.PaginatedCats.Data)
                {
                    var cat = catsWithTag1.FirstOrDefault(c => c.CatId == catDto.CatId);
                    Assert.NotNull(cat);
                    Assert.Equal(cat.CatId, catDto.CatId);
                    Assert.Equal(cat.Width, catDto.Width);
                    Assert.Equal(cat.Height, catDto.Height);
                    Assert.Equal(cat.Image, catDto.Image);
                    Assert.Contains(cat.Tags, ct => ct.Name == tag1.Name);
                }

                return true;
            },
            onFailure: error =>
            {
                Assert.Fail("Expected success but got failure");
                return false;
            });
    }

    private List<Cat> CreateCatsWithTag(Tag tag, int count)
    {
        var cats = new List<Cat>();
        for (int i = 1; i <= count; i++)
        {
            var cat = Cat.Create($"cat{i}", 100 + i, 200 + i, new byte[] { }, new List<Tag> { tag });
            cats.Add(cat);
        }
        return cats;
    }

    [Fact]
    public async Task Handle_NoCatsExist_ReturnsEmptyPaginatedCats()
    {
        // Arrange
        var tag = "cute";
        var page = 1;
        var pageSize = 10;
        var cats = new List<Cat>();
        var totalCount = 0;
        _mockCatRepository.GetPaginatedCatsByTagAsync(page, pageSize, tag, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult((cats, totalCount)));

        var query = new GetPaginatedCatsByTagQuery(page, pageSize, tag);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Match(
            onSuccess: response =>
            {
                Assert.Equal(totalCount, response.PaginatedCats.Count);
                Assert.Empty(response.PaginatedCats.Data);
                return true;
            },
            onFailure: error =>
            {
                Assert.Fail("Expected success but got failure");
                return false;
            });
    }
}
