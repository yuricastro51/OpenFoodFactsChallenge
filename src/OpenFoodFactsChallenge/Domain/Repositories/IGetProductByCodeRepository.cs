using OpenFoodFactsChallenge.Domain.Entities;

namespace OpenFoodFactsChallenge.Domain.Repositories;

public interface IGetProductByCodeRepository
{
    Task<Product?> Get(long code, CancellationToken cancellationToken);
}