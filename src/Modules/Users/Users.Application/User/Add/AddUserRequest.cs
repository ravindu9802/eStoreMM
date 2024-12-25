using Users.Domain.Enums;

namespace Users.Application.User.Add;

public record AddUserRequest(string FirstName, string LastName, string Email, UserRole Role);