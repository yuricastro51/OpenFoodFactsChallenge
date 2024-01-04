using MediatR;
using OpenFoodFactsChallenge.Domain.Queries.Response;

namespace OpenFoodFactsChallenge.Domain.Queries.Requests;

public class GetProductByCodeRequest : IRequest<GetProductByCodeResponse>
{
    public long Code { get; set; }
}