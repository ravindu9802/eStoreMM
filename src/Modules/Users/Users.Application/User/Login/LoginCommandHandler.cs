using MediatR;
using Users.Application.Abstractions;
using Users.Domain.Repositories;

namespace Users.Application.User.Login;

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IJwtProvider _provider;
    private readonly IUserRepository _repository;

    public LoginCommandHandler(IUserRepository repository, IJwtProvider provider)
    {
        _repository = repository;
        _provider = provider;
    }

    public async Task<string?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // 1. Get User
        var user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is null) return null;

        // 2. Create Token
        var token = _provider.Generate(user);

        // 3. Return Token
        return token;
    }
}