namespace Products.Domain.Repositories;

public interface IProductUoW
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}