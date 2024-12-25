using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using Products.Domain.Repositories;
using Products.Infrastructure.Persistance;

namespace Products.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        return product.Id;
    }

    public async Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await _context.Products.FindAsync(id, cancellationToken);
        if (item is not null)
        {
            _context.Products.Remove(item);
            return true;
        }

        return false;
    }

    public async Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public Task<bool> UpdateProductAsync(Guid id, Product product, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}