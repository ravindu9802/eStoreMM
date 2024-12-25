using MediatR;

namespace Users.Application.User.Login;

public record LoginCommand(string Email, string Password) : IRequest<string>;