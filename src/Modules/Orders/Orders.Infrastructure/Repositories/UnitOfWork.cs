using Orders.Domain.Repositories;
using Orders.Infrastructure.Persistance;

namespace Orders.Infrastructure.Repositories;

public class UnitOfWork : IOrderUoW
{
    private readonly OrderDbContext _context;

    public UnitOfWork(OrderDbContext context)
    {
        _context = context;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}