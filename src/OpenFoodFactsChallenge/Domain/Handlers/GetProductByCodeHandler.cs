using MediatR;
using OpenFoodFactsChallenge.Domain.Queries.Requests;
using OpenFoodFactsChallenge.Domain.Queries.Response;
using OpenFoodFactsChallenge.Domain.Repositories;

namespace OpenFoodFactsChallenge.Domain.Handlers;

public class GetProductByCodeHandler : IRequestHandler<GetProductByCodeRequest, GetProductByCodeResponse>
{
    private readonly IGetProductByCodeRepository _getProductByCodeRepository;

    public GetProductByCodeHandler(IGetProductByCodeRepository getProductByCodeRepository)
    {
        _getProductByCodeRepository = getProductByCodeRepository;
    }

    public async Task<GetProductByCodeResponse> Handle(GetProductByCodeRequest request, CancellationToken cancellationToken)
    {
        var product = await _getProductByCodeRepository.Get(request.Code, cancellationToken);

        if (product is null)
            return null!;

        return new GetProductByCodeResponse
        {
            ProductName = product.ProductName,
            Quantity = product.Quantity,
            Categories = product.Categories,
            Packaging = product.Packaging,
            Brands = product.Brands,
            ImageUrl = product.ImageUrl,
            Barcode = product.Barcode,
            Status = product.Status,
            ImportedT = product.ImportedT,
            Url = product.Url,
            Code = product.Code
        };
    }
}