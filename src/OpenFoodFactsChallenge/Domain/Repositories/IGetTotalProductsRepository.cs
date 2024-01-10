namespace OpenFoodFactsChallenge.Domain.Repositories;

public interface IGetTotalProductsRepository
{
    Task<int> Get(CancellationToken cancellationToken);
}