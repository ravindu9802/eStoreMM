using SharedKernel.Primitives;
using Users.Domain.Enums;

namespace Users.Domain.Entities;

public class User : AggregateRoot
{
    public User(Guid id, string firstName, string lastName, string email, UserRole role) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
}