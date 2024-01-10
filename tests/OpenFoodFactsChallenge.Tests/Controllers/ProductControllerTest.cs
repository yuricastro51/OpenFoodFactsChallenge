using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenFoodFactsChallenge.Controllers;
using OpenFoodFactsChallenge.Domain.Entities;
using OpenFoodFactsChallenge.Domain.Queries.Requests;
using OpenFoodFactsChallenge.Domain.Queries.Response;
using OpenFoodFactsChallenge.Domain.Repositories;
using OpenFoodFactsChallenge.Helpers.Http;

namespace OpenFoodFactsChallenge.Tests.Controllers;

public class ProductControllerTest
{
    private readonly ProductController _sut;
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<IGetProductListRepository> _getProductListRepository;

    public ProductControllerTest()
    {
        _mediator = new Mock<IMediator>();
        _getProductListRepository = new Mock<IGetProductListRepository>();
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

        _mediator.Setup(x => x.Send(It.IsAny<GetProductByCodeRequest>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("ex"));

        var result = (ServerError)await _sut.GetProductByCode(code, default);

        result.StatusCode.Should().Be(500);
        result.Value.Should().Be(new ServerError("Internal server error", "ex").Value);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async void GetProductByCode_ShouldReturn400OnInvalidCode(long code)
    {
        var result = (BadRequest)await _sut.GetProductByCode(code, default);

        result.StatusCode.Should().Be(400);
        result.Value.Should().Be(new BadRequest("Invalid param: code").Value);
    }

    [Fact]
    public async void GetProductByCode_ShouldCallMediatorWithCorrectValue()
    {
        var code = 1234567890123;

        await _sut.GetProductByCode(code, default);

        _mediator.Verify(x => x.Send(It.IsAny<GetProductByCodeRequest>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async void GetProductList_ShouldReturn200OnSuccess()
    {
        var response = new GetProductListResponse
        {
            CurrentPage = 1,
            Total = 2,
            Skip = 0,
            Take = 10,
            Data =
                new[]
                {
                    new Product(
                        1L,
                        "1234567890123",
                        "https://world.openfoodfacts.org/product/1234567890123",
                        "Product Name",
                        "1 kg",
                        "Category 1, Category 2",
                        "Packaging 1, Packaging 2",
                        "Brand 1, Brand 2",
                        "https://static.openfoodfacts.org/images/products/1234567890123/front_en.123.400.jpg"),
                    new Product(
                        2L,
                        "1234567890124",
                        "https://world.openfoodfacts.org/product/1234567890124",
                        "Product Name 2",
                        "2 kg",
                        "Category 1, Category 2",
                        "Packaging 1, Packaging 2",
                        "Brand 1, Brand 2",
                        "https://static.openfoodfacts.org/images/products/1234567890124/front_en.123.400.jpg")
                }
        };

        _mediator.Setup(x => x.Send(It.IsAny<GetProductListRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = (OkObjectResult)await _sut.GetProducts(default);

        result.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async void GetProductList_ShouldReturn500OnException()
    {
        _mediator.Setup(x => x.Send(It.IsAny<GetProductListRequest>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("ex"));

        var result = (ServerError)await _sut.GetProducts(default);

        result.StatusCode.Should().Be(500);
        result.Value.Should().Be(new ServerError("Internal server error", "ex").Value);
    }

    [Fact]
    public async void GetProductList_MediatrWithCorrectValues()
    {
        var skip = 0;
        var take = 10;

        await _sut.GetProducts(default, skip, take);

        _mediator.Verify(x => x.Send(It.Is<GetProductListRequest>(r => r.Skip == skip && r.Take == take), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async void GetProductList_ShouldReturnAnEmptyListWhenHasNoProduct()
    {
        var response = new GetProductListResponse
        {
            CurrentPage = 0,
            Total = 0,
            Skip = 0,
            Take = 10,
            Data = Array.Empty<Product>()
        };

        _mediator.Setup(x => x.Send(It.IsAny<GetProductListRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = (OkObjectResult)await _sut.GetProducts(default);

        result.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async void GetProductList_ShouldReturn400OnInvalidSkip()
    {
        var skip = -1;
        var take = 10;
        var result = (BadRequest)await _sut.GetProducts(default, skip, take);

        result.StatusCode.Should().Be(400);
        result.Value.Should().Be(new BadRequest("Invalid param: skip").Value);
    }

    [Theory]
    [InlineData(0, -1)]
    [InlineData(10, 0)]
    public async void GetProductList_ShouldReturn400OnInvalidTake(int skip, int take)
    {
        var result = (BadRequest)await _sut.GetProducts(default, skip, take);

        result.StatusCode.Should().Be(400);
        result.Value.Should().Be(new BadRequest("Invalid param: take").Value);
    }

    [Fact]
    public async void GetProductList_ShouldReturn400OnInvalidSkipAndTake()
    {
        var skip = -1;
        var take = 0;
        var result = (BadRequest)await _sut.GetProducts(default, skip, take);

        result.StatusCode.Should().Be(400);
        result.Value.Should().Be(new BadRequest("Invalid param: skip").Value);
    }
}

