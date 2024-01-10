using FluentAssertions;
using Moq;
using OpenFoodFactsChallenge.Domain.Entities;
using OpenFoodFactsChallenge.Domain.Handlers;
using OpenFoodFactsChallenge.Domain.Queries.Requests;
using OpenFoodFactsChallenge.Domain.Repositories;

namespace OpenFoodFactsChallenge.Tests.Domain.Handlers;

public class GetProductByCodeHandlerTest
{
    private readonly GetProductByCodeHandler _sut;
    private readonly Mock<IGetProductByCodeRepository> _productRepositoryMock;

    public GetProductByCodeHandlerTest()
    {
        _productRepositoryMock = new Mock<IGetProductByCodeRepository>();
        _sut = new GetProductByCodeHandler(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnProduct_WhenProductExists()
    {
        var code = 789L;
        var cancellationToken = new CancellationToken();

        _productRepositoryMock.Setup(x => x.Get(code, cancellationToken)).ReturnsAsync(
            new Product(code, "barcode", "url", "productName", "quantity", "categories", "packaging", "brands", "imageUrl"));

        var result = await _sut.Handle(new GetProductByCodeRequest { Code = code }, cancellationToken);

        result.Should().NotBeNull();
        result.Code.Should().Be(code);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenProductDoesNotExist()
    {
        var code = 789L;
        var cancellationToken = new CancellationToken();

        var result = await _sut.Handle(new GetProductByCodeRequest { Code = code }, cancellationToken);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenCodeIsInvalid()
    {
        var code = 0L;
        var cancellationToken = new CancellationToken();

        var result = await _sut.Handle(new GetProductByCodeRequest { Code = code }, cancellationToken);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_ShouldCallGetProductByCodeRepository_WithCorrectValue()
    {
        var code = 789L;
        var cancellationToken = new CancellationToken();

        await _sut.Handle(new GetProductByCodeRequest { Code = code }, cancellationToken);

        _productRepositoryMock.Verify(x => x.Get(code, cancellationToken), Times.Once);
    }
}