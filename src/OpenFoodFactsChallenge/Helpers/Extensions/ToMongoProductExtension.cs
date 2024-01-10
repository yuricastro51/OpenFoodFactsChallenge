using MongoDB.Bson;
using OpenFoodFactsChallenge.Domain.Entities;
using OpenFoodFactsChallenge.Infrastructure.Models;

namespace OpenFoodFactsChallenge.Helpers.Extensions;

public static class ToMongoProductExtension
{
    public static MongoProduct ToMongoProduct(this Product product) {
        return new MongoProduct
        {
            Id = ObjectId.GenerateNewId(),
            Code = product.Code,
            Barcode = product.Barcode,
            Brands = product.Brands,
            Categories = product.Categories,
            ImageUrl = product.ImageUrl,
            ImportedT = DateTime.Now,
            Packaging = product.Packaging,
            ProductName = product.ProductName,
            Quantity = product.Quantity,
            Status = EStatus.Imported,
            Url = product.Url
        };
    }
}