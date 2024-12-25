namespace Orders.Domain.Repositories;

public interface IOrderUoW
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}