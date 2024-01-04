using FluentAssertions;
using OpenFoodFactsChallenge.Domain.Entities;

namespace OpenFoodFactsChallenge.Tests.Domain.Entities;

public class ProductTests
{
    [Fact]
    public void Instantiate()
    {
        var validData = new
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


        var product = new Product(
            validData.Code,
            validData.Barcode,
            validData.Url,
            validData.ProductName,
            validData.Quantity,
            validData.Categories,
            validData.Packaging,
            validData.Brands,
            validData.ImageUrl);

        product.Should().NotBeNull();
        product.Code.Should().Be(validData.Code);
        product.Barcode.Should().Be(validData.Barcode);
        product.Url.Should().Be(validData.Url);
        product.ProductName.Should().Be(validData.ProductName);
        product.Quantity.Should().Be(validData.Quantity);
        product.Categories.Should().Be(validData.Categories);
        product.Packaging.Should().Be(validData.Packaging);
        product.Brands.Should().Be(validData.Brands);
        product.ImageUrl.Should().Be(validData.ImageUrl);
        product.Status.Should().Be(EStatus.Draft);
        product.ImportedT.Should().Be(default);
    }

    [Fact]
    public void ShouldSetStatusToImported()
    {
        var validData = new
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


        var product = new Product(
            validData.Code,
            validData.Barcode,
            validData.Url,
            validData.ProductName,
            validData.Quantity,
            validData.Categories,
            validData.Packaging,
            validData.Brands,
            validData.ImageUrl);

        product.SetImported();

        product.Status.Should().Be(EStatus.Imported);
        product.ImportedT.Should().NotBe(default);
    }
}