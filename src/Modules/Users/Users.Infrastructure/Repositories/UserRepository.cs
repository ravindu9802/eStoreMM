using Microsoft.EntityFrameworkCore;
using Users.Domain.Entities;
using Users.Domain.Repositories;
using Users.Infrastructure.Persistance;

namespace Users.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        return user.Id;
    }

    public async Task<bool> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await _context.Users.FindAsync(id, cancellationToken);
        if (item is not null)
        {
            _context.Users.Remove(item);
            return true;
        }

        return false;
    }

    public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public Task<bool> UpdateUserAsync(Guid id, User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}