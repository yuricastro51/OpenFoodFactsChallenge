using MediatR;
using OpenFoodFactsChallenge.Domain.Queries.Response;

namespace OpenFoodFactsChallenge.Domain.Queries.Requests;

public class GetProductListRequest : IRequest<GetProductListResponse>
{
    public int Skip { get; set; }
    public int Take { get; set; }
}