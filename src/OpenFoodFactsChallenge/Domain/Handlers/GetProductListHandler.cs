using MediatR;
using OpenFoodFactsChallenge.Domain.Queries.Requests;
using OpenFoodFactsChallenge.Domain.Queries.Response;
using OpenFoodFactsChallenge.Domain.Repositories;

namespace OpenFoodFactsChallenge.Domain.Handlers;

public class GetProductListHandler : IRequestHandler<GetProductListRequest, GetProductListResponse>
{
    private readonly IGetProductListRepository _getProductListRepository;
    private readonly IGetTotalProductsRepository _getTotalProductsRepository;

    public GetProductListHandler(
        IGetProductListRepository getProductListRepository,
        IGetTotalProductsRepository getTotalProductsRepository)
    {
        _getProductListRepository = getProductListRepository;
        _getTotalProductsRepository = getTotalProductsRepository;
    }

    public async Task<GetProductListResponse> Handle(GetProductListRequest request, CancellationToken cancellationToken)
    {
        var total = await _getTotalProductsRepository.Get(cancellationToken);
        var products = await _getProductListRepository.Get(request.Skip, request.Take, cancellationToken);
        return new GetProductListResponse
        {
            CurrentPage = request.Skip / request.Take + 1,
            Total = total,
            Skip = request.Skip,
            Take = request.Take,
            Data = products
        };
    }
}