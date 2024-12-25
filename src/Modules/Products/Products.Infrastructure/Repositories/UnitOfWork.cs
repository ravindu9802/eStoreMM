using Products.Domain.Repositories;
using Products.Infrastructure.Persistance;

namespace Products.Infrastructure.Repositories;

public class UnitOfWork : IProductUoW
{
    private readonly ProductDbContext _context;

    public UnitOfWork(ProductDbContext context)
    {
        _context = context;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}