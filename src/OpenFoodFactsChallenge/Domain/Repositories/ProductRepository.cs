using Microsoft.EntityFrameworkCore;
using OpenFoodFactsChallenge.Domain.Entities;
using OpenFoodFactsChallenge.Helpers.Extensions;
using OpenFoodFactsChallenge.Infrastructure.Contexts;

namespace OpenFoodFactsChallenge.Domain.Repositories;

public class ProductRepository : IGetProductByCodeRepository, IAddProductRepository, IGetProductListRepository, IGetTotalProductsRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> Get(long code, CancellationToken cancellationToken)
    {
        var mongoProduct = await _context.Products.FirstOrDefaultAsync(p => p.Code == code, cancellationToken);
        if (mongoProduct == null)
        {
            return null;
        }

        return mongoProduct.ToProduct();
    }

    public async Task Add(Product product, CancellationToken cancellationToken)
    {
        await _context.Products.AddAsync(product.ToMongoProduct(), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<Product[]> Get(int skip, int take, CancellationToken cancellationToken)
    {
        var mongoProducts = _context.Products.Skip(skip).Take(take).ToListAsync(cancellationToken);
        return mongoProducts.ContinueWith(products => products.Result.Select(p => p.ToProduct()).ToArray(), cancellationToken);
    }

    public async Task<int> Get(CancellationToken cancellationToken)
    {
        var count = await _context.Products.CountAsync(cancellationToken);
        return count;
    }
}