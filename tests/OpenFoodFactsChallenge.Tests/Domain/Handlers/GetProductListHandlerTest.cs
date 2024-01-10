using FluentAssertions;
using Moq;
using OpenFoodFactsChallenge.Domain.Handlers;
using OpenFoodFactsChallenge.Domain.Queries.Requests;
using OpenFoodFactsChallenge.Domain.Queries.Response;
using OpenFoodFactsChallenge.Domain.Repositories;

namespace OpenFoodFactsChallenge.Tests.Domain.Handlers;

public class GetProductListHandlerTest
{
    private readonly Mock<IGetProductListRepository> _getProductListRepositoryMock;
    private readonly Mock<IGetTotalProductsRepository> _getTotalProductsRepositoryMock;
    private readonly GetProductListHandler _sut;

    public GetProductListHandlerTest()
    {
        _getProductListRepositoryMock = new Mock<IGetProductListRepository>();
        _getTotalProductsRepositoryMock = new Mock<IGetTotalProductsRepository>();
        _sut = new GetProductListHandler(_getProductListRepositoryMock.Object, _getTotalProductsRepositoryMock.Object);
    }

    [Fact]
    public async Task ShouldReturnGetProductListResponse()
    {
        var result = await _sut.Handle(
            new GetProductListRequest
            {
                Skip = 0,
                Take = 10
            },
            CancellationToken.None);

        result.Should().BeOfType<GetProductListResponse>();
        result.Take.Should().Be(10);
        result.Skip.Should().Be(0);
        result.Total.Should().Be(0);
        result.CurrentPage.Should().Be(1);
        result.Data.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldCallGetTotalProductsRepository()
    {
        await _sut.Handle(
            new GetProductListRequest
            {
                Skip = 0,
                Take = 10
            },
            CancellationToken.None);

        _getTotalProductsRepositoryMock.Verify(x => x.Get(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ShouldCallGetProductListRepository()
    {
        await _sut.Handle(
            new GetProductListRequest
            {
                Skip = 0,
                Take = 10
            },
            CancellationToken.None);

        _getProductListRepositoryMock.Verify(x => x.Get(0, 10, CancellationToken.None), Times.Once);
    }
}