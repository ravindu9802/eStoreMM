namespace Users.Application.Abstractions;

public interface IJwtProvider
{
    string Generate(Domain.Entities.User user);
}