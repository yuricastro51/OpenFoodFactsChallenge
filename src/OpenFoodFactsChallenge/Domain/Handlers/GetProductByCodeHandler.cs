using MediatR;
using OpenFoodFactsChallenge.Domain.Queries.Requests;
using OpenFoodFactsChallenge.Domain.Queries.Response;

namespace OpenFoodFactsChallenge.Domain.Handlers;

public class GetProductByCodeHandler : IRequestHandler<GetProductByCodeRequest, GetProductByCodeResponse>
{
    public Task<GetProductByCodeResponse> Handle(GetProductByCodeRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult((GetProductByCodeResponse)null!);
    }
}