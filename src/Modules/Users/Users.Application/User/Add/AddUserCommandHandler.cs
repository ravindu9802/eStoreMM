using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Users.Domain.Repositories;

namespace Users.Application.User.Add;

internal sealed class AddUserCommandHandler : IRequestHandler<AddUserCommand, Guid?>
{
    private readonly IBus _bus;
    private readonly ILogger<AddUserCommandHandler> _logger;
    private readonly IUserRepository _repository;
    private readonly IUserUoW _unitOfWork;

    public AddUserCommandHandler(IUserRepository repository, IUserUoW unitOfWork, IBus bus,
        ILogger<AddUserCommandHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _bus = bus;
        _logger = logger;
    }

    public async Task<Guid?> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request {@RequestName}, {@DatetimeUtc}", request, DateTime.UtcNow);

        var res = await _repository.AddUserAsync(request.User, cancellationToken);
        _logger.LogInformation("User created. {@UserId}, {@User}, {@DatetimeUtc}", res, request.User, DateTime.UtcNow);

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("User persisted. {@UserId}, {@User}, {@DatetimeUtc}", res, request.User,
            DateTime.UtcNow);

        var addUserEvent = new AddUserEvent
            { EventId = Guid.NewGuid(), UserId = res, Email = request.User.Email };
        await _bus.Publish(addUserEvent);
        _logger.LogInformation("Event published. {@AddUserEvent}, {@DatetimeUtc}", addUserEvent, DateTime.UtcNow);

        return res;
    }
}