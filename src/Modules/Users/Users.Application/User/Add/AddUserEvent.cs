namespace Users.Application.User.Add;

public record AddUserEvent
{
    public Guid EventId { get; init; }
    public Guid UserId { get; init; }
    public string Email { get; init; }
    public DateTime OccuredAtUtc { get; init; } = DateTime.UtcNow;
}