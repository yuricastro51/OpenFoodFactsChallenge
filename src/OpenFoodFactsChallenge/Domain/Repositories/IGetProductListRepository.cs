using OpenFoodFactsChallenge.Domain.Entities;

namespace OpenFoodFactsChallenge.Domain.Repositories;

public interface IGetProductListRepository
{
    Task<Product[]> Get(int skip, int take, CancellationToken cancellationToken);
}