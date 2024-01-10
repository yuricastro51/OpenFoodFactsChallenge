using OpenFoodFactsChallenge.Domain.Entities;

namespace OpenFoodFactsChallenge.Domain.Services;

public interface IWebScrapingService
{
    List<Product> Scrap();
}