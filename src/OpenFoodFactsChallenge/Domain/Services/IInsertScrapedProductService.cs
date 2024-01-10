namespace OpenFoodFactsChallenge.Domain.Services;

public interface IInsertScrapedProductService
{
    Task Insert(CancellationToken cancellationToken);
}