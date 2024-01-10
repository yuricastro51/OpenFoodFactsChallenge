using OpenFoodFactsChallenge.Domain.Entities;

namespace OpenFoodFactsChallenge.Domain.Queries.Response;

public class GetProductListResponse
{
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public int CurrentPage { get; set; }
    public Product[]? Data { get; set; }
}