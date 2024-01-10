using OpenFoodFactsChallenge.Domain.Entities;

namespace OpenFoodFactsChallenge.Domain.Repositories;

public interface IAddProductRepository
{
    Task Add(Product product, CancellationToken cancellationToken);
}