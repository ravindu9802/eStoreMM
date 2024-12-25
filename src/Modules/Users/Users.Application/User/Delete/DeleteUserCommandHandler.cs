using MediatR;
using Users.Domain.Repositories;

namespace Users.Application.User.Delete;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _repository;
    private readonly IUserUoW _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository repository, IUserUoW unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var res = await _repository.DeleteUserAsync(request.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return res;
    }
}