using Orders.Domain.Entities;
using Orders.Domain.Enums;

namespace Orders.Domain.Repositories;

public interface IOrderRepository
{
    Task<Guid?> CreateOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default);

    Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus orderStatus,
        CancellationToken cancellationToken = default);
}