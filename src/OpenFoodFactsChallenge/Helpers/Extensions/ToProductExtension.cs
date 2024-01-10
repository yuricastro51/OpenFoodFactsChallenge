using OpenFoodFactsChallenge.Domain.Entities;
using OpenFoodFactsChallenge.Infrastructure.Models;

namespace OpenFoodFactsChallenge.Helpers.Extensions;

public static class ToProductExtension
{
    public static Product ToProduct (this MongoProduct product) {
        return new Product(
            product.Code,
            product.Barcode,
            product.Url ?? string.Empty,
            product.ProductName ?? string.Empty,
            product.Quantity ?? string.Empty,
            product.Categories ?? string.Empty,
            product.Packaging ?? string.Empty,
            product.Brands ?? string.Empty,
            product.ImageUrl ?? string.Empty);
    }       
}