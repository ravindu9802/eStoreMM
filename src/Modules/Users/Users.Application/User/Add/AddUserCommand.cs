using MediatR;

namespace Users.Application.User.Add;

public record AddUserCommand(Domain.Entities.User User) : IRequest<Guid?>;