using OpenFoodFactsChallenge.Domain.Repositories;

namespace OpenFoodFactsChallenge.Domain.Services;

public class InsertScrapedProductsService : IInsertScrapedProductService
{
    private readonly IAddProductRepository _addProductRepository;
    private readonly IWebScrapingService _webScrapingService;
    private readonly IGetProductByCodeRepository _getProductByCodeRepository;

    public InsertScrapedProductsService(
        IAddProductRepository addProductRepository,
        IGetProductByCodeRepository getProductByCodeRepository,
        IWebScrapingService webScrapingService)
    {
        _addProductRepository = addProductRepository;
        _getProductByCodeRepository = getProductByCodeRepository;
        _webScrapingService = webScrapingService;
    }

    public async Task Insert(CancellationToken cancellationToken)
    {
        var products = _webScrapingService.Scrap();
        foreach (var product in products)
        {
            var productExists = await _getProductByCodeRepository.Get(product.Code, cancellationToken);
            if (productExists is not null)
            {
                continue;
            }
            product.SetImported();
            await _addProductRepository.Add(product, cancellationToken);
        }
    }
}