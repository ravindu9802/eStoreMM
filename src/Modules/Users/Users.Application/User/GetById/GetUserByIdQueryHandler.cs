using MediatR;
using Users.Domain.Repositories;

namespace Users.Application.User.GetById;

internal sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Domain.Entities.User?>
{
    private readonly IUserRepository _repository;

    public GetUserByIdQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetUserByIdAsync(request.Id);
    }
}