using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenFoodFactsChallenge.Controllers;
using OpenFoodFactsChallenge.Domain.Entities;
using OpenFoodFactsChallenge.Domain.Queries.Requests;
using OpenFoodFactsChallenge.Domain.Queries.Response;
using OpenFoodFactsChallenge.Helpers.Http;

namespace OpenFoodFactsChallenge.Tests.Controllers;

public class ProductControllerTest
{
    private readonly ProductController _sut;
    private readonly Mock<IMediator> _mediator;

    public ProductControllerTest()
    {
        _mediator = new Mock<IMediator>();
        _sut = new ProductController(_mediator.Object);
    }

    [Fact]
    public async void GetProductByCode_ShouldReturn404IfNotFound()
    {
        _mediator.Setup(x => x.Send(It.IsAny<GetProductByCodeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync((GetProductByCodeResponse)null!);

        var code = 1234567890123;
        
        var result = (NotFound)await _sut.GetProductByCode(code, default);

        result.StatusCode.Should().Be(404);
        result.Value.Should().Be(new NotFound("Product not found").Value);
    }

    [Fact]
    public async void GetProductByCode_ShouldReturn200OnSuccess()
    { 
        var code = 1234567890123;

        var response = new GetProductByCodeResponse
        {
            Code = 1L,
            Barcode = "1234567890123",
            Url = "https://world.openfoodfacts.org/product/1234567890123",
            ProductName = "Product Name",
            Quantity = "1 kg",
            Categories = "Category 1, Category 2",
            Packaging = "Packaging 1, Packaging 2",
            Brands = "Brand 1, Brand 2",
            ImageUrl = "https://static.openfoodfacts.org/images/products/1234567890123/front_en.123.400.jpg"
        };

        _mediator.Setup(x => x.Send(It.IsAny<GetProductByCodeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = (OkObjectResult)await _sut.GetProductByCode(code, default);

        result.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async void GetProductByCode_ShouldReturn500OnException()
    {
        var code = 1234567890123;

        _mediator.Setup(x => x.Send(It.IsAny<GetProductByCodeRequest>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        var result = (ServerError)await _sut.GetProductByCode(code, default);

        result.StatusCode.Should().Be(500);
        result.Value.Should().Be(new ServerError("Internal server error").Value);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async void GetProductByCode_ShouldReturn400OnInvalidCode(long code)
    {
        var result = (BadRequest)await _sut.GetProductByCode(code, default);

        result.StatusCode.Should().Be(400);
        result.Value.Should().Be(new BadRequest("Invalid code").Value);
    }

    [Fact]
    public async void GetProductByCode_ShouldCallMediatorWithCorrectValue()
    {
        var code = 1234567890123;

        await _sut.GetProductByCode(code, default);

        _mediator.Verify(x => x.Send(It.IsAny<GetProductByCodeRequest>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}

