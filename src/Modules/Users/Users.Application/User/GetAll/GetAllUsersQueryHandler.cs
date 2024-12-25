using MediatR;
using Users.Domain.Repositories;

namespace Users.Application.User.GetAll;

internal sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<Domain.Entities.User>>
{
    private readonly IUserRepository _repository;

    public GetAllUsersQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Domain.Entities.User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllUsersAsync(cancellationToken);
    }
}