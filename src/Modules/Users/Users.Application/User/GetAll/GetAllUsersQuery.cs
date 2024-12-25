using MediatR;

namespace Users.Application.User.GetAll;

public record GetAllUsersQuery : IRequest<List<Domain.Entities.User>>;