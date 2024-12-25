using MediatR;

namespace Users.Application.User.GetById;

public record GetUserByIdQuery(Guid Id) : IRequest<Domain.Entities.User?>;