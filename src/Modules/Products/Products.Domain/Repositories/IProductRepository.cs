using Products.Domain.Entities;

namespace Products.Domain.Repositories;

public interface IProductRepository
{
    Task<Guid> AddProductAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdateProductAsync(Guid id, Product product, CancellationToken cancellationToken = default);
    Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken = default);
}