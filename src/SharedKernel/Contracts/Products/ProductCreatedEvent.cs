namespace SharedKernel.Contracts.Products;

public record ProductCreatedEvent
{
    public Guid ProductId { get; init; }
    public DateTime OccuredAtUtc { get; init; } = DateTime.UtcNow;
}