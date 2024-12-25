using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Domain.Enums;
using Orders.Domain.Repositories;
using Orders.Infrastructure.Persistance;

namespace Orders.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Guid?> CreateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        return order.Id;
    }

    public async Task<Order?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
    }

    public async Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus orderStatus,
        CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order is null) return false;

        order.ChangeOrderStatus(orderStatus);

        return true;
    }
}