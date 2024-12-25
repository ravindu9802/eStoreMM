using MediatR;

namespace Users.Application.User.Delete;

public record DeleteUserCommand(Guid Id) : IRequest<bool>;