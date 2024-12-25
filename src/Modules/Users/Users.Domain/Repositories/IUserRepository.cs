using Users.Domain.Entities;

namespace Users.Domain.Repositories;

public interface IUserRepository
{
    Task<Guid> AddUserAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdateUserAsync(Guid id, User user, CancellationToken cancellationToken = default);
    Task<bool> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);
}