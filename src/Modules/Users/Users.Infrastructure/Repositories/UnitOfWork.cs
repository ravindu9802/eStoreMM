using Users.Domain.Repositories;
using Users.Infrastructure.Persistance;

namespace Users.Infrastructure.Repositories;

public class UnitOfWork : IUserUoW
{
    private readonly UserDbContext _context;

    public UnitOfWork(UserDbContext context)
    {
        _context = context;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}