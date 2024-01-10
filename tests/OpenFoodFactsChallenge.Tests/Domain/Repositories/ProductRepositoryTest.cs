using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OpenFoodFactsChallenge.Domain.Entities;
using OpenFoodFactsChallenge.Domain.Repositories;
using OpenFoodFactsChallenge.Infrastructure.Contexts;
using OpenFoodFactsChallenge.Infrastructure.Models;
using OpenFoodFactsChallenge.Tests.Infrastructure.Contexts;

namespace OpenFoodFactsChallenge.Tests.Domain.Repositories;

public class ProductRepositoryTest
{
    private readonly ProductRepository _sut;
    private readonly AppDbContext _context;

    public ProductRepositoryTest()
    {
        _context = InMemoryDatabaseContext.GetDbContext();
        _context.Database.EnsureDeleted();
        _sut = new ProductRepository(_context);
    }

    [Fact]
    public async Task Get_ShouldReturnProduct_WhenProductExists()
    {
        var code = 789;
        var cancellationToken = new CancellationToken();

        _context.Products.Add(new MongoProduct
        {
            Code = code,
            Barcode = "123",
            Brands = "brands",
            Categories = "categories",
            ImageUrl = "imageUrl",
            ImportedT = DateTime.Now,
            Packaging = "packaging",
            ProductName = "productName",
            Quantity = "quantity",
            Status = EStatus.Imported,
            Url = "url"
        });
        await _context.SaveChangesAsync(cancellationToken);

        var result = await _sut.Get(code, cancellationToken);

        result.Should().NotBeNull();
        result.Code.Should().Be(code);
    }

    [Fact]
    public async Task Get_ShouldReturnNull_WhenProductDoesNotExist()
    {
        var code = 789;
        var cancellationToken = new CancellationToken();

        var result = await _sut.Get(code, cancellationToken);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Get_ShouldReturnNull_WhenCodeIsInvalid()
    {
        var code = 0;
        var cancellationToken = new CancellationToken();

        var result = await _sut.Get(code, cancellationToken);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Add_ShouldAddProduct_WhenProductDoesNotExist()
    {
        var cancellationToken = new CancellationToken();
        var product = new Product(
            789,
            "123",
            "url",
            "productName",
            "quantity",
            "categories",
            "packaging",
            "brands",
            "imageUrl");

        await _sut.Add(product, cancellationToken);

        var result = await _context.Products.FirstOrDefaultAsync(p => p.Code == product.Code, cancellationToken);

        result.Should().NotBeNull();
        result.Code.Should().Be(product.Code);
    }
}